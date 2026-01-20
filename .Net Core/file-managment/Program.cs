using AppFiles.Classes;

// DEFINIR VARIABLES

// Name of the new folder: Febrero
string category = "09-03-2025";

// Category Tags
string tags = "[Joa][Lara][Casa][Planos][Impresiones]";

// From the folder
string sourcePath = "C:\\Users\\luzzi\\OneDrive\\Documentos\\Docs Sweet Docs\\Imagenes\\New Images";

// erase duplicates
Files.DeleteFilesWithDuplicateHashes(sourcePath);

// move to a new folder using a catigory to the folder name
Files.RenameFilesBasedOnCreationTime(sourcePath, category, tags);

// make a note into a log file, for example: in this category I have image from..
string text = """
    Hay varias imagenes importantes recuperada de cuando joa y lara eran chicos
    imagenes de planos de la casa
    impresiones de pantallas
    y algunas otras imagenes que hay que revisar
""";

Files.WriteLog("C:\\Users\\luzzi\\OneDrive\\Documentos\\Docs Sweet Docs\\Imagenes\\personal notes.log", text, category);

