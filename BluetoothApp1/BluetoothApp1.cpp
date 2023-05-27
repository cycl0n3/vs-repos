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

    // Iterate through all available adapters
    std::vector<BLUETOOTH_RADIO_INFO> radioInfos;
    BLUETOOTH_RADIO_INFO radioInfo;
    radioInfo.dwSize = sizeof(BLUETOOTH_RADIO_INFO);
    radioInfos.push_back(radioInfo);
    do
    {
        if (BluetoothGetRadioInfo(hRadio, &radioInfos[0]) == ERROR_SUCCESS)
        {
            std::string manufacturer = bluetooth_manufacturers[radioInfos[0].manufacturer];
            std::cout << "Adapter: " << radioInfos[0].szName << std::endl;
            std::cout << "Manufacturer: " << manufacturer << std::endl;
            std::cout << "Address: " << radioInfos[0].address.ullLong << std::endl;
        }
    } while (BluetoothFindNextRadio(hFind, &hRadio));

    return 0;
}
