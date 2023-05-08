#pragma once

#include <iostream>
#include <vector>
#include <algorithm>
#include <cmath>

class PrimeList
{
public:
    PrimeList(int n);
    ~PrimeList();
    void populate();

    friend std::ostream& operator<<(std::ostream& os, const PrimeList& pl);

    static void example();
private:
    std::vector<int> primes;
    int n;
};
