#!/bin/bash

# Check for correct usage
if [ "$#" -ne 1 ]; then
    echo "Usage: $0 <folder_path>"
    exit 1
fi

FOLDER_PATH="$1"
LAST_FOLDER=$("$FOLDER_PATH")

# Check if the folder exists
if [ ! -d "$FOLDER_PATH" ]; then
    echo "Error: Folder '$FOLDER_PATH' does not exist."
    exit 1
fi

# Iterate through files and rename them based on properties
find "$FOLDER_PATH" -type f | while read -r FILE; do
    CREATION_DATE=$(stat -c%y "$FILE") 
    # Extract file properties
    BASENAME=$(basename "$FILE")           # File name
    EXTENSION="${BASENAME##*.}"            # File extension
    #FILENAME="${BASENAME%.*}"              # File name without extension
    FILESIZE=$(stat -c%s "$FILE")          # File size in bytes
    #CREATION_DATE=$(stat -c%y "$FILE")     # File creation date (full format)
    CREATION_DAY=$(date -d "$CREATION_DATE" +%d) # Extract year from date
    CREATION_MONTH=$(date -d "$CREATION_DATE" +%m) # Extract month from date
    CREATION_YEAR=$(date -d "$CREATION_DATE" +%Y) # Extract year from date
    CREATION_HH=$(date -d "$CREATION_DATE" +%H) # Extract year from date
    CREATION_MM=$(date -d "$CREATION_DATE" +%M) # Extract year from date
    CREATION_SS=$(date -d "$CREATION_DATE" +%S) # Extract year from date
    
    # Define a new name pattern, e.g., YYYY-MM-FILENAME-SIZE.EXT
    NEW_NAME="[${CREATION_YEAR} ${CREATION_MONTH} ${CREATION_DAY}][${CREATION_HH}:${CREATION_MM}:${CREATION_SS}] ${LAST_FOLDER} [${FILESIZE}].${EXTENSION}"

    # Get directory name
    DIRNAME=$(dirname "$FILE")

    # Avoid overwriting files
    if [ "$BASENAME" != "$NEW_NAME" ]; then
        mv "$DIRNAME/$BASENAME" "$DIRNAME/$NEW_NAME"
        echo "Renamed: $BASENAME -> $NEW_NAME"
    fi
done

echo "Renaming complete!"
