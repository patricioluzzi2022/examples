from PIL import Image
import numpy as np
import pandas as pd

# Load the image
image_path = "./files/input/matriz 2d 62x26.png"
image = Image.open(image_path).convert("RGB")

# Convert image to numpy array (height x width x 3)
image_array = np.array(image)

# Confirm dimensions
image_array.shape

# Convertimos cada píxel (fila x columna) a una tupla RGB
df_rgb = pd.DataFrame([[tuple(pixel) for pixel in row] for row in image_array])

print(df_rgb[0][25])
print(df_rgb[61][25])
print(df_rgb[61][0])
print(df_rgb[0][0])

