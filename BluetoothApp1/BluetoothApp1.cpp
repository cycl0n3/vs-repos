#include <iostream>
#include <windows.h>
#include <bluetoothapis.h>
#include <bthdef.h>
#include <bthsdpdef.h>
#include <ws2bth.h>
#include <string>
#include <vector>
#include <algorithm>
#include <unordered_map>

#include "bluetooth_manufacturers.h"


int main()
{
    // Get a list of all available adapters
    std::vector<BLUETOOTH_FIND_RADIO_PARAMS> radioParams;
    BLUETOOTH_FIND_RADIO_PARAMS radioParam;
    radioParam.dwSize = sizeof(BLUETOOTH_FIND_RADIO_PARAMS);
    radioParams.push_back(radioParam);
    HANDLE hRadio = NULL;
    HBLUETOOTH_RADIO_FIND hFind = BluetoothFindFirstRadio(&radioParams[0], &hRadio);
    if (hFind == NULL)
    {
        std::cout << "No Bluetooth adapters found." << std::endl;
        return 0;
    }

    BLUETOOTH_RADIO_INFO radioInfo;
    radioInfo.dwSize = sizeof(BLUETOOTH_RADIO_INFO);
    
    if (BluetoothGetRadioInfo(hRadio, &radioInfo) == ERROR_SUCCESS)
    {
        std::string manufacturer = bluetooth_manufacturers[radioInfo.manufacturer];
        std::cout << "Adapter: " << radioInfo.szName << std::endl;
        std::cout << "Manufacturer: " << manufacturer << std::endl;
        std::cout << "Address: " << radioInfo.address.ullLong << std::endl;
    }
    else
    {
        std::cout << "Error getting adapter info." << std::endl;
    }

    // Get a list of all paired devices
    BLUETOOTH_DEVICE_INFO deviceInfo;
    deviceInfo.dwSize = sizeof(BLUETOOTH_DEVICE_INFO);
    
    BLUETOOTH_DEVICE_SEARCH_PARAMS searchParams;
    searchParams.dwSize = sizeof(BLUETOOTH_DEVICE_SEARCH_PARAMS);
    searchParams.fReturnAuthenticated = TRUE;
    searchParams.fReturnConnected = FALSE;
    searchParams.fReturnRemembered = TRUE;
    searchParams.fReturnUnknown = TRUE;
    searchParams.fIssueInquiry = FALSE;

    HBLUETOOTH_DEVICE_FIND hFindDevice = BluetoothFindFirstDevice(&searchParams, &deviceInfo);
    
    if (hFindDevice == NULL)
    {
        std::cout << "No paired devices found." << std::endl;
        return 0;
    }
    else
    {
        std::cout << "Paired devices:" << std::endl;
        do
        {
            std::cout << "Name: " << deviceInfo.szName << std::endl;
            std::cout << "Address: " << deviceInfo.Address.ullLong << std::endl;
            std::cout << "Class: " << deviceInfo.ulClassofDevice << std::endl;
            std::cout << "Connected: " << deviceInfo.fConnected << std::endl;
            std::cout << "Authenticated: " << deviceInfo.fAuthenticated << std::endl;
            std::cout << "Remembered: " << deviceInfo.fRemembered << std::endl;
            std::cout << std::endl;
            deviceInfo.dwSize = sizeof(BLUETOOTH_DEVICE_INFO);
        } while (BluetoothFindNextDevice(hFindDevice, &deviceInfo));
    }

    return 0;
}
