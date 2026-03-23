$baseUrl = "http://localhost:5000"
$results = @{}

function Test-Endpoint {
    param($method, $path, $body = $null, $headers = @{})
    try {
        $params = @{
            Uri = "$baseUrl$path"
            Method = $method
            ContentType = "application/json"
            Headers = $headers
            TimeoutSec = 10
        }
        if ($body) { $params.Body = $body | ConvertTo-Json }
        $response = Invoke-RestMethod @params
        return @{ success = $true; data = $response }
    } catch {
        return @{ success = $false; error = $_.Exception.Message; status = $_.Exception.Response.StatusCode.value__ }
    }
}

# Wait for server
Write-Host "Waiting for server to start..."
$maxRetries = 20
$retryCount = 0
while ($retryCount -lt $maxRetries) {
    try {
        $tcp = New-Object System.Net.Sockets.TcpClient
        $tcp.Connect("localhost", 5000)
        $tcp.Close()
        Write-Host "Server is up!"
        break
    } catch {
        $retryCount++
        Start-Sleep -Seconds 3
    }
}

if ($retryCount -eq $maxRetries) {
    $results.Error = "Server failed to start within timeout"
} else {
    # 1. DB Status
    $results.DbStatus = Test-Endpoint "GET" "/api/authentication/db-status"

    # 2. Customer Login
    $loginModel = @{ username = "thanhthuy2003"; password = "Thanhthuy@2003" }
    $results.CustomerLogin = Test-Endpoint "POST" "/api/authentication/login" $loginModel

    # 3. Admin Login
    $adminLoginModel = @{ username = "admin"; password = "Admin@123" }
    $results.AdminLogin = Test-Endpoint "POST" "/api/authentication/dashboard/login" $adminLoginModel

    # 4. Products List
    $results.Products = Test-Endpoint "GET" "/api/products"
}

$results | ConvertTo-Json -Depth 10 | Out-File -FilePath "test_results_detailed.json"
