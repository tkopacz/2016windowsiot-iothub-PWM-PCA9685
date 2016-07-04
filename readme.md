# Windows IoT + Lighting + PWM + Servo
Tested on:
http://www.dx.com/p/keyestudio-16-channel-12-bit-pwm-servo-driver-w-i2c-interface-419722

Used PCA9685, 12 bit, 16 channels, I2C

[Datasheet PCA9685](http://cache.nxp.com/documents/data_sheet/PCA9685.pdf?fpsp=1&WT_TYPE=Data Sheets&WT_VENDOR=FREESCALE&WT_FILE_FORMAT=pdf&WT_ASSET=Documentation&fileExt=.pdf)

Should work also with:
https://www.adafruit.com/product/815
or
http://www.hobbytronics.co.uk/pwm-servo

Lighting Library:
[https://github.com/ms-iot/lightning](https://github.com/ms-iot/lightning)

### Remarks ###
1. Servo colors:

	**Power**: Red
	**Ground**: Black, Brown
	**Signal**: Yellow, White, Orange, Gray… well, can be any color

2. Do not forget to connect power to PCA9685 board!
3. Do not forget to switch Windows IoT to Direct Memory Mapped Driver (see: https://developer.microsoft.com/en-us/windows/iot/win10/lightningsetup ) 

4. New project, do not forget to add (manually) to **Package.appxmanifest**:
   
		<Package 
		  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10" 
		  xmlns:mp="http://schemas.microsoft.com/appx/2014/phone/manifest" 
		  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10" 
		  xmlns:iot="http://schemas.microsoft.com/appx/manifest/iot/windows10"
		  IgnorableNamespaces="uap mp iot">
		...
		
		  <Capabilities>
		    <Capability Name="internetClient" />
		    <iot:Capability Name="lowLevelDevices" />
		    <DeviceCapability Name="109b86ad-f53d-4b76-aa5f-821e2ddf2141"/>
		  </Capabilities>
		</Package>
