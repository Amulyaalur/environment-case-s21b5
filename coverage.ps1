param($linerate)

function WriteXmlToScreen ([xml]$xml)
{
    $StringWriter = New-Object System.IO.StringWriter;
    $XmlWriter = New-Object System.Xml.XmlTextWriter $StringWriter;
    $XmlWriter.Formatting = "indented";
    $xml.WriteTo($XmlWriter);
    $XmlWriter.Flush();
    $StringWriter.Flush();
    Write-Output $StringWriter.ToString();
}

$report_of_sender = Get-Content -Path  Sensor.Tests\TestResults\*\coverage.cobertura.xml | Out-String
Write-Host "---------------------------------"
Write-Host "Code Coverage report of SensorTest ..." 
Write-Host "---------------------------------"
WriteXmlToScreen $report_of_sender

[xml]$doc_sender = $report_of_sender

Write-Host ""
Write-Host "---------------------------------"
Write-Host "Code Coverage report of SensorTest Analysis..." 
Write-Host "---------------------------------"

$result_sender = 0

Write-Host "Line-Coverage: ["$doc_sender.coverage.'line-rate'"] Branch-Coverage: ["$doc_sender.coverage.'branch-rate'"]"
Write-Host ""
 foreach ($pkg in $doc_sender.coverage.packages.package) {
    Write-Host "Package:"  $pkg.name "Line-Coverage:"$pkg.'line-rate'

    if($pkg.'line-rate' -lt $linerate){
        $result_sender= 1
       }
    }


$report_of_receiver = Get-Content -Path  EnvironmentMonitor.Tests\TestResults\*\coverage.cobertura.xml | Out-String
Write-Host "---------------------------------"
Write-Host "Code Coverage report of EnvironmentMonitorTest ..." 
Write-Host "---------------------------------"
WriteXmlToScreen $report_of_receiver

[xml]$doc_receiver = $report_of_receiver

Write-Host ""
Write-Host "---------------------------------"
Write-Host "Code Coverage report of EnvironmentMonitorTest Analysis..." 
Write-Host "---------------------------------"

$result_receiver = 0

Write-Host "Line-Coverage: ["$doc_receiver.coverage.'line-rate'"] Branch-Coverage: ["$doc_receiver.coverage.'branch-rate'"]"
Write-Host ""
 foreach ($pkg in $doc_receiver.coverage.packages.package) {
    Write-Host "Package:"  $pkg.name "Line-Coverage:"$pkg.'line-rate'

    if($pkg.'line-rate' -lt $linerate){
        $result_receiver= 1
       }
    }

if(($result_sender -eq 1) -or ($result_receiver -eq 1)){
    Write-Host "Coverage Check: failed" -ForegroundColor red 
    exit 1
}
else{
    Write-Host "Coverage Check: Passed" -ForegroundColor green 
    exit 0
}
