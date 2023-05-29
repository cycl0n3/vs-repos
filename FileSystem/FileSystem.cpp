// FileSystem.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <filesystem>
#include <fstream>
#include <string>
#include <vector>

int main()
{
    std::string directory = "C:\\Users\\Millind\\source\\repos";

    std::filesystem::path path = directory;

    std::vector<std::filesystem::directory_entry> files;

    // list all files in directory
    for (const auto& entry : std::filesystem::directory_iterator(path))
    {
        files.push_back(entry);
    }

    // print all files in directory
    for (const auto& file : files)
    {
        std::cout << file.path() << std::endl;
    }
}
