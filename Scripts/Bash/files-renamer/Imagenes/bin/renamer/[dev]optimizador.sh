#!/bin/bash

# Check for correct usage
if [ "$#" -ne 1 ]; then
    echo "Usage: $0 <folder_path>"
    exit 1
fi

FOLDER_PATH="$1"

# Check if the folder exists
if [ ! -d "$FOLDER_PATH" ]; then
    echo "Error: Folder '$FOLDER_PATH' does not exist."
    exit 1
fi

# Use associative array for tracking files by size
declare -A file_map

# Find all files in the folder and process them
find "$FOLDER_PATH" -type f | while read -r FILE; do
    FILESIZE=$(stat -c%s "$FILE")       # Get file size
    MODTIME=$(stat -c%Y "$FILE")       # Get modification time (epoch)
    BASENAME=$(basename "$FILE")       # Get the file name

    # Check if a file of the same size already exists
    if [[ -n "${file_map[$FILESIZE]}" ]]; then
        # Compare modification times
        EXISTING_FILE="${file_map[$FILESIZE]}"
        EXISTING_MODTIME=$(stat -c%Y "$EXISTING_FILE")

        if [[ $MODTIME -lt $EXISTING_MODTIME ]]; then
            # Current file is older, keep it
            echo "Keeping: $FILE (older than $EXISTING_FILE)"
            rm "$EXISTING_FILE"
            file_map[$FILESIZE]="$FILE"
        else
            # Existing file is older, delete the current one
            echo "Deleting: $FILE (newer than $EXISTING_FILE)"
            rm "$FILE"
        fi
    else
        # First file of this size, add it to the map
        file_map[$FILESIZE]="$FILE"
    fi
done

echo "Comparison complete. Duplicates removed."

