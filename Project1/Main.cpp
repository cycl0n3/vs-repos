#include <iostream>

#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc.hpp>

int main()
{
    cv::Mat image = cv::Mat::zeros(300, 600, CV_8UC3);
    
    cv::circle(image, cv::Point(250, 150), 100, cv::Scalar(0, 255, 128), -100);
    cv::circle(image, cv::Point(350, 150), 100, cv::Scalar(255, 255, 255), -100);

    cv::imshow("Display", image);
    cv::waitKey(0);

    return 0;
}
