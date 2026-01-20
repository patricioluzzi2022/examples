Import-Module SqlServer

# Variables de conexión
$sqlServer   = "database-server-server.database.windows.net"    # O solo IP/hostname si es la instancia por defecto
$database    = "data_analysis_book"
$dbUser      = "usuario_servicio_sql_database"
$dbPassword  = "Pass1325TestBDB!"

$connectionString = "Server=$sqlServer;Database=$database;User ID=$dbUser;Password=$dbPassword;Encrypt=True;TrustServerCertificate=True"

# Ruta al script .sql que deseas ejecutar
$directories = @(
    "C:\Users\luzzi\OneDrive\Desktop\Develop\ABookiz\Database Scripts\Schemas",
    "C:\Users\luzzi\OneDrive\Desktop\Develop\ABookiz\Database Scripts\Stored Procedures",
    "C:\Users\luzzi\OneDrive\Desktop\Develop\ABookiz\Database Scripts\Functions",
    "C:\Users\luzzi\OneDrive\Desktop\Develop\ABookiz\Database Scripts\Triggers",
    "C:\Users\luzzi\OneDrive\Desktop\Develop\ABookiz\Database Scripts\Mock Data"
)

foreach ($dir in $directories) {
    # Obtener todos los archivos .sql en ese directorio (opcional: con -Recurse para subcarpetas)
    $scripts = Get-ChildItem -Path $dir -Filter *.sql

    foreach ($script in $scripts) {
        Write-Host "Ejecutando script: $($script.FullName)"
        
        Invoke-Sqlcmd `
            -ServerInstance $sqlServer `
            -Database $database `
            -Username $dbUser `
            -Password $dbPassword `
            -TrustServerCertificate `
            -InputFile $script.FullName
    }
}
