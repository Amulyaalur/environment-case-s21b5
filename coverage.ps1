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

$report_of_sender = Get-Content -Path  Sender.Tests\TestResults\*\coverage.cobertura.xml | Out-String
Write-Host "---------------------------------"
Write-Host "Code Coverage report of SenderTest ..." 
Write-Host "---------------------------------"
WriteXmlToScreen $report_of_sender

[xml]$doc_send = $report_of_sender

Write-Host ""
Write-Host "---------------------------------"
Write-Host "Code Coverage report of ReceiverTest Analysis..." 
Write-Host "---------------------------------"

$result_send = 0

Write-Host "Line-Coverage: ["$doc_send.coverage.'line-rate'"] Branch-Coverage: ["$doc_send.coverage.'branch-rate'"]"
Write-Host ""
 foreach ($pkg in $doc_send.coverage.packages.package) {
    Write-Host "Package:"  $pkg.name "Line-Coverage:"$pkg.'line-rate'

    if($pkg.'line-rate' -lt $linerate){
        $result_send= 1
       }
    }

if($result_send -eq 1){
    Write-Host "Coverage Check: failed" -ForegroundColor red 
}
else{
    Write-Host "Coverage Check: Passed" -ForegroundColor green 
}
exit $result_send
