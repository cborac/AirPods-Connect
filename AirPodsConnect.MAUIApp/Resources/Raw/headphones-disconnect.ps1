$headphonesName = "AirPods Pro Avrcp Transport"

$bluetoothDevices = Get-PnpDevice -class Bluetooth

$headphonePnpDevices = $bluetoothDevices | Where-Object { $_.Name.StartsWith("$headphonesName") }

if(!$headphonePnpDevices) {
    Write-Host "Coudn't find any devices related to the '$headphonesName'"
    Write-Host "Whole list of available devices is:"
    $bluetoothDevices
    return
}

Write-Host "The following PnP devices related to the '$headphonesName' headphones found:"
ForEach($d in $headphonePnpDevices) { $d }

Write-Host "`nDisable all these devices"
ForEach($d in $headphonePnpDevices) { Disable-PnpDevice -InstanceId $d.InstanceId -Confirm:$false }