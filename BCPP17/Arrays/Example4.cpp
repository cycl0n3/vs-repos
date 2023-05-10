#include "Example4.h"

#include <iostream>
#include <string>
#include <iomanip>

// include boost
#include <boost/asio.hpp>


Example4::Example4()
{
}

Example4::~Example4()
{
}

void Example4::Run()
{
    // print two dimensional multiplication table
    int table[10][10];

    for (int row = 0; row < 10; row++)
    {
        for (int column = 0; column < 10; column++)
        {
            table[row][column] = (row + 1) * (column + 1);
        }
    }

    for (int row = 0; row < 10; row++)
    {
        for (int column = 0; column < 10; column++)
        {
            std::cout << std::setw(4) << table[row][column];
        }
        std::cout << std::endl;
    }

    std::cout << std::endl;

    std::string url = "https://api.publicapis.org/entries";
    boost::asio::io_context io_context;
    boost::asio::ip::tcp::resolver resolver(io_context);
    boost::asio::ip::tcp::resolver::results_type endpoints = resolver.resolve(url, "80");
    boost::asio::ip::tcp::socket socket(io_context);
    boost::asio::connect(socket, endpoints);

    boost::asio::streambuf request;
    std::ostream request_stream(&request);
    request_stream << "GET " << url << " HTTP/1.0\r\n";
    request_stream << "Host: " << url << "\r\n";
    request_stream << "Accept: */*\r\n";
    request_stream << "Connection: close\r\n\r\n";

    boost::asio::write(socket, request);

    boost::asio::streambuf response;
    boost::asio::read_until(socket, response, "\r\n");

    std::istream response_stream(&response);
    std::string http_version;
    response_stream >> http_version;
    unsigned int status_code;
    response_stream >> status_code;
    std::string status_message;
    std::getline(response_stream, status_message);

    if (!response_stream || http_version.substr(0, 5) != "HTTP/")
    {
        std::cout << "Invalid response\n";
        return;
    }

    if (status_code != 200)
    {
        std::cout << "Response returned with status code " << status_code << "\n";
        return;
    }

    boost::asio::read_until(socket, response, "\r\n\r\n");

    std::string header;
    while (std::getline(response_stream, header) && header != "\r")
    {
        std::cout << header << "\n";
    }
    std::cout << "\n";

    std::string json;
    boost::system::error_code error;
    while (boost::asio::read(socket, response, boost::asio::transfer_at_least(1), error))
    {
        std::istream response_stream(&response);
        std::getline(response_stream, json);
        std::cout << json << "\n";
    }

    if (error != boost::asio::error::eof)
    {
        throw boost::system::system_error(error);
    }

    std::cout << "\n";

    std::cout << "Press any key to continue...";
    std::cin.get();
}