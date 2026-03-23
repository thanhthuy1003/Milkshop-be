$body = @{
    username = "thanhthuy678"
    password = "Thanhthuy@2003"
} | ConvertTo-Json
try {
    $response = Invoke-RestMethod -Uri "http://localhost:5000/api/authentication/login" -Method Post -Body $body -ContentType "application/json"
    $response | ConvertTo-Json -Depth 5
} catch {
    Write-Output $_.Exception.Message
    $_.Exception.Response | ConvertTo-Json
}
