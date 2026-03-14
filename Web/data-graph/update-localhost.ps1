Copy-Item -Path "./assets" -Destination "C:\inetpub\wwwroot" -Recurse -Force

Copy-Item -Path "./index.html" -Destination "C:\inetpub\wwwroot" -Force

Write-Host "Copia finalizada"