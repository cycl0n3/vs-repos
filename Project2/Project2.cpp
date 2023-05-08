#include <iostream>

// import opencv header files
#include <opencv2/core.hpp>
#include <opencv2/highgui.hpp>
#include <opencv2/imgproc.hpp>

int main()
{
    cv::Mat shops;
    shops = cv::imread("shops.png", cv::IMREAD_COLOR);

    if (shops.empty())
    {
        std::cout << "Could not read the image: " << "shops.png" << std::endl;
        return 1;
    }

    cv::resize(shops, shops, cv::Size(), 0.5, 0.5);

    cv::namedWindow("Shops", cv::WINDOW_AUTOSIZE);
    cv::imshow("Shops", shops);

    cv::Mat shops_gray;
    cv::cvtColor(shops, shops_gray, cv::COLOR_BGR2GRAY);
    cv::rotate(shops_gray, shops_gray, cv::ROTATE_90_CLOCKWISE);

    cv::namedWindow("Shops Gray", cv::WINDOW_AUTOSIZE);
    cv::imshow("Shops Gray", shops_gray);

    cv::waitKey(0);
    return 0;
}
