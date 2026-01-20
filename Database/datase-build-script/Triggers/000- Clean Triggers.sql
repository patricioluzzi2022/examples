USE [data_analysis_book];
GO

DECLARE @tableName SYSNAME;
DECLARE @sqlCmd NVARCHAR(MAX);

-- 1) Declarar el cursor con la lista de tablas de interés
DECLARE curso_Tablas CURSOR FOR
    SELECT name 
    FROM sys.tables
    -- Puedes filtrar con WHERE (por ejemplo WHERE name LIKE 'Prefijo%')
    ORDER BY name;

-- 2) Abrir el cursor
OPEN curso_Tablas;

FETCH NEXT FROM curso_Tablas INTO @tableName;
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Texto fijo para los nombres de los triggers
    DECLARE @triggerInsertSuffix NVARCHAR(50) = 'TriggerAferInsert';
    DECLARE @triggerUpdateSuffix NVARCHAR(50) = 'TriggerAferUpdate';

    -- Generar el comando para eliminar el trigger 'TriggerAferInsert'
    SET @sqlCmd = N'
        IF EXISTS (SELECT name FROM sys.triggers WHERE name = N''' + @tableName + @triggerInsertSuffix + ''')
        BEGIN
            DROP TRIGGER ' + QUOTENAME(@tableName + @triggerInsertSuffix) + ';
        END
    ';
    PRINT @sqlCmd;
    EXEC sp_executesql @sqlCmd;

    -- Generar el comando para eliminar el trigger 'TriggerAferUpdate'
    SET @sqlCmd = N'
        IF EXISTS (SELECT name FROM sys.triggers WHERE name = N''' + @tableName + @triggerUpdateSuffix + ''')
        BEGIN
            DROP TRIGGER ' + QUOTENAME(@tableName + @triggerUpdateSuffix) + ';
        END
    ';
    PRINT @sqlCmd;
    EXEC sp_executesql @sqlCmd;

    -- Siguiente fila
    FETCH NEXT FROM curso_Tablas INTO @tableName;
END;

-- 4) Cerrar y desechar el cursor
CLOSE curso_Tablas;
DEALLOCATE curso_Tablas;