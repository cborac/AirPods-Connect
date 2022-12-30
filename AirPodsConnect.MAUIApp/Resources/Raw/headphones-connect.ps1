$headphonesName = "Avrcp Transport"

$bluetoothDevices = Get-PnpDevice -class Bluetooth

$headphonePnpDevices = $bluetoothDevices | Where-Object { $_.Name.EndsWith("$headphonesName") }

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

Write-Host "Enable all these devices"
ForEach($d in $headphonePnpDevices) { Enable-PnpDevice -InstanceId $d.InstanceId -Confirm:$false }

Write-Host "The headphones should be connected now."

Write-Host "It may take around 10 seconds until the Windows starts showing audio devices related to these headphones."