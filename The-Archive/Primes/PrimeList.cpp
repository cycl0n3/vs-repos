#include "PrimeList.h"

PrimeList::PrimeList(int n) : n(n)
{
    populate();
}

PrimeList::~PrimeList()
{
}

// calculate first n primes
void PrimeList::populate()
{
    primes.push_back(2);
    primes.push_back(3);
    primes.push_back(5);

    int i = 7;
    
    while (primes.size() < n)
    {
        bool isPrime = true;
        
        for (int j = 0; j < primes.size(); j++)
        {
            if (i % primes[j] == 0)
            {
                isPrime = false;
                break;
            }
        }
        
        if (isPrime)
        {
            primes.push_back(i);
        }
        
        i += 2;
    }
}

// print like [2, 3, 5] 
std::ostream& operator<<(std::ostream& os, const PrimeList& pl)
{
    os << "[";
    for (int i = 0; i < pl.primes.size(); i++)
    {
        os << pl.primes[i];
        if (i < pl.primes.size() - 1)
        {
            os << ", ";
        }
    }
    os << "]";
    return os;
}

void PrimeList::example()
{
    PrimeList pl(100);
    std::cout << pl << std::endl;
}
