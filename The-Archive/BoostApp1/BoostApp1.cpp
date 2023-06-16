#include <iostream>
#include <vector>

#include <boost/accumulators/accumulators.hpp>
#include <boost/accumulators/statistics/stats.hpp>
#include <boost/accumulators/statistics/mean.hpp>
#include <boost/accumulators/statistics/moment.hpp>

using namespace boost::accumulators;

int main()
{
    std::vector<double> v = { 1.2, 2.3, 3.4, 4.5 };

    // Display the results ...
    std::cout << "Mean:   " << mean(v) << std::endl;
    std::cout << "Moment: " << moment<2>(v) << std::endl;
    std::cout << "Standard Deviation:   " << std::sqrt(moment<2>(v)) << std::endl;

    return 0;
}
