USE [data_analysis_book];
go

Declare @tableName sysname;
Declare @sqlCommand nvarchar(max);
Declare @trigger sysname;
Declare @sqlCmd nvarchar(max);

-- 1) Declarar el cursor con la lista de tablas de interés
Declare curso_Tablas cursor For
    SELECT name 
    FROM sys.tables
    -- Puedes filtrar con WHERE (por ejemplo WHERE name LIKE 'Prefijo%')
    Order by name;

-- 2) Abrir el cursor
OPEN curso_Tablas;

-- 3) Recorrer el cursor
FETCH NEXT FROM curso_Tablas INTO @tableName;
WHILE @@Fetch_status = 0
BEGIN

    SELECT TOP 1 
        @trigger = t.name
    FROM   sys.triggers t
    INNER JOIN sys.tables tb ON t.parent_id = tb.object_id
    WHERE  tb.name = @tableName
    -- Si el schema es distinto a dbo, filtrarlo también: AND SCHEMA_NAME(tb.schema_id) = 'MiSchema'
    ORDER BY t.name; -- Por si existiera más de un trigger

    IF @trigger IS NOT NULL
    BEGIN
        SET @sqlCmd = N'DROP TRIGGER ' + QUOTENAME(@tableName) + N'.' + QUOTENAME(@trigger);
        PRINT @sqlCmd;
        EXEC(@sqlCmd);
    END
    ELSE
    BEGIN
        PRINT N'No se encontró ningún trigger para la tabla ' + @tableName;
    END

    -- Siguiente fila
    Fetch Next From curso_Tablas Into @tableName;
END;

-- 4) Cerrar y desechar el cursor
CLOSE      curso_Tablas;
DEALLOCATE curso_Tablas;


-- 1) Declarar el cursor con la lista de tablas de interés
DECLARE curso_Tablas CURSOR FOR
    SELECT name 
    FROM sys.tables
    -- Puedes filtrar con WHERE (por ejemplo WHERE name LIKE 'Prefijo%')
    ORDER BY name;

-- 2) Abrir el cursor
OPEN curso_Tablas;

-- 3) Recorrer el cursor
FETCH NEXT FROM curso_Tablas INTO @tableName;
WHILE @@FETCH_STATUS = 0
BEGIN

    SET @sqlCmd = N'
            CREATE OR ALTER TRIGGER ' + QUOTENAME(@tableName + 'TriggerAferInsert') + N'
            ON dbo.' + QUOTENAME(@tableName) + N'
            AFTER INSERT
            AS
            BEGIN
                SET NOCOUNT ON;

                DECLARE @Id INT;
                SELECT @Id = Id FROM inserted;

                UPDATE ' + QUOTENAME(@tableName) + '
                SET CreationDate = GETDATE()
                WHERE Id = @Id AND CreationDate IS NULL;

            END;';

    PRINT @sqlCmd;
    EXEC sp_executesql @sqlCmd;

    SET @sqlCmd = N'
            CREATE OR ALTER TRIGGER ' + QUOTENAME(@tableName + 'TriggerAferUpdate') + N'
            ON dbo.' + QUOTENAME(@tableName) + N'
            AFTER UPDATE
            AS
            BEGIN
                SET NOCOUNT ON;

                DECLARE @Id INT;
                SELECT @Id = Id FROM inserted;

                UPDATE ' + QUOTENAME(@tableName) + '
                SET ModificationDate = GETDATE()
                WHERE Id = @Id;

            END;';

    PRINT @sqlCmd;
    EXEC sp_executesql @sqlCmd;

    -- Siguiente fila
    FETCH NEXT FROM curso_Tablas INTO @tableName;
END;

-- 4) Cerrar y desechar el cursor
CLOSE curso_Tablas;
DEALLOCATE curso_Tablas;