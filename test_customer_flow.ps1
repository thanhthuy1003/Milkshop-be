$baseUrl = "http://localhost:5000"
$loginUrl = "$baseUrl/api/authentication/login"
$profileUrl = "$baseUrl/api/authentication/profile"
$cartUrl = "$baseUrl/api/carts"
$productsUrl = "$baseUrl/api/products"

$credentials = @{
    username = "thanhthuy2003"
    password = "Thanhthuy@2003"
}

Write-Host "--- Testing Customer Login ---"
try {
    $loginResponse = Invoke-RestMethod -Uri $loginUrl -Method Post -Body ($credentials | ConvertTo-Json) -ContentType "application/json"
    $token = $loginResponse.data.accessToken
    Write-Host "Login Successful. Token obtained."
} catch {
    Write-Host "Login Failed: $_"
    exit
}

$headers = @{
    "Authorization" = "Bearer $token"
    "Accept" = "application/json"
}

Write-Host "`n--- Testing Products API (No Auth) ---"
try {
    $products = Invoke-RestMethod -Uri $productsUrl -Method Get
    Write-Host "Products API Successful. Found $($products.data.items.Count) items."
} catch {
    Write-Host "Products API Failed: $_"
}

Write-Host "`n--- Testing Customer Profile ---"
try {
    $profile = Invoke-RestMethod -Uri $profileUrl -Method Get -Headers $headers
    Write-Host "Profile API Successful. Name: $($profile.data.firstName) $($profile.data.lastName)"
} catch {
    Write-Host "Profile API Failed: $_"
}

Write-Host "`n--- Testing Customer Cart ---"
try {
    $cart = Invoke-RestMethod -Uri $cartUrl -Method Get -Headers $headers
    Write-Host "Cart API Successful."
} catch {
    Write-Host "Cart API Failed: $_"
}
