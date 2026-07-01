#!/bin/bash
# Arranca SQL Server en background
/opt/mssql/bin/sqlservr &
PID=$!

# Espera a que esté listo
echo "Esperando SQL Server..."
for i in {1..30}; do
  /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -No \
    -Q "SELECT 1" &>/dev/null && break
  sleep 2
done

# Ejecuta scripts de inicialización
echo "Inicializando base de datos..."
for f in /docker-entrypoint-initdb.d/*.sql; do
  /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -No -i "$f"
done

echo "Listo."
wait $PID