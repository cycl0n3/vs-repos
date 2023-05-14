#include <iostream>

// import opencv libraries
#include <opencv2/core.hpp>
#include <opencv2/highgui.hpp>
#include <opencv2/imgproc.hpp>

int main()
{
    // read image
    cv::Mat originalImage = cv::imread("images/hibiscus.jpg");

    // reduce height by 0.75
    cv::resize(originalImage, originalImage, cv::Size(), 0.35, 0.35);

    // apply simple bilateral filter
    cv::Mat bilateralImage;
    cv::bilateralFilter(originalImage, bilateralImage, 2, 50, 5);

    // apply gaussian filter
    cv::Mat gaussianImage;
    cv::GaussianBlur(originalImage, gaussianImage, cv::Size(5, 5), 0);

    // median blur
    cv::Mat medianImage;
    cv::medianBlur(originalImage, medianImage, 5);

    // display images
    cv::imshow("Original Image", originalImage);
    cv::imshow("Bilateral Filtered Image", bilateralImage);
    cv::imshow("Gaussian Filtered Image", gaussianImage);
    cv::imshow("Median Filtered Image", medianImage);

    cv::waitKey(0);

    return 0;
}
