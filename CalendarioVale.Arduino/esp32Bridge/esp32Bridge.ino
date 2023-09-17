#include <WiFi.h>
#include <HTTPClient.h>
#include <BLEDevice.h>
#define bleServerName "S5-BC88"
#define ssid "MartinRouterKing"
#define password "3nr1c0boscolo"
#define serverName "https://192.168.14.120:7227/api/biometrics"
#define bmeServiceUUID "0000ff00-0000-1000-8000-00805f9b34fb"

static boolean connected = false;
static BLEAddress *pServerAddress;

bool connectToServer(BLEAddress pAddress)
{
  BLEClient *pClient = BLEDevice::createClient();
  if (pClient->connect(pAddress))
  {
    BLERemoteService *pRemoteService = pClient->getService(bmeServiceUUID);
    if (pRemoteService != nullptr)
    {
      // Successfully connected to the BLE server
      // Add your characteristic registration here if needed
      return true;
    }
  }
  // Failed to connect or find the service
  return false;
}

class MyAdvertisedDeviceCallbacks : public BLEAdvertisedDeviceCallbacks
{
  void onResult(BLEAdvertisedDevice advertisedDevice)
  {
    if (advertisedDevice.getName() == bleServerName)
    {
      advertisedDevice.getScan()->stop();
      if (pServerAddress != nullptr)
      {
        delete pServerAddress;
      }
      pServerAddress = new BLEAddress(advertisedDevice.getAddress());
      connected = false;
    }
  }
};

void setup()
{
  Serial.begin(115200);
  WiFi.mode(WIFI_STA); // Optional
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(100);
  }
  BLEDevice::init("");
  BLEScan *pBLEScan = BLEDevice::getScan();
  pBLEScan->setAdvertisedDeviceCallbacks(new MyAdvertisedDeviceCallbacks());
  pBLEScan->setActiveScan(true);
  pBLEScan->start(30);
}

static void temperatureNotifyCallback(BLERemoteCharacteristic* pBLERemoteCharacteristic, 
                                        uint8_t* pData, size_t length, bool isNotify) {
  //store temperature value
  temperatureChar = (char*)pData;
  newTemperature = true;
}

void loop()
{
  Serial.println("ping of life");
  if (connected == false)
  {
    if (connectToServer(*pServerAddress))
    {
      Serial.println("connected");
      // Activate the Notify property of each Characteristic
      connected = true;
    }
    connected = false;
  }
  if (WiFi.status() == WL_CONNECTED)
  {
    HTTPClient http;
    http.addHeader("Content-Type", "application/json");
    http.begin(serverName);
    String postBody = "{\"value\":" + String(50);
    postBody += ",\"type\":" + String(0);
    postBody += "}";
    http.POST(postBody);
    http.end();
  }
  delay(1000);
}