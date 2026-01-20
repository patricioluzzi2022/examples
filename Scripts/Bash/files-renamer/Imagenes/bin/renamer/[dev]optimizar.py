import cv2
import os
import numpy as np
from sklearn.cluster import DBSCAN
from skimage.metrics import structural_similarity as ssim

def load_images(folder):
    images = []
    filenames = []
    for file in os.listdir(folder):
        if file.endswith(('.jpg', '.png', '.jpeg')):
            filepath = os.path.join(folder, file)
            img = cv2.imread(filepath)
            img = cv2.resize(img, (200, 200))  # Resize for uniformity
            gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)  # Convert to grayscale
            images.append(gray)
            filenames.append(filepath)
    return images, filenames

folder_path = '/home/smooth/Documents/Imagenes'
print('Path:')
print(folder_path)
print('Loading images..')
images, filenames = load_images(folder_path)
print('Images loaded')

def compute_histogram(image):
    hist = cv2.calcHist([image], [0], None, [256], [0, 256])
    hist = cv2.normalize(hist, hist).flatten()
    return hist

print('Computing histograms..')
features = [compute_histogram(img) for img in images]
print('Histograms Computed')

def compare_images(img1, img2):
    # Structural Similarity Index (SSIM)
    similarity, _ = ssim(img1, img2, full=True)
    return similarity

print('Comparing images matrix..')
# Example similarity matrix (optional for visualization)
similarity_matrix = np.zeros((len(images), len(images)))

for i in range(len(images)):
    for j in range(len(images)):
        similarity_matrix[i][j] = compare_images(images[i], images[j])

# Use DBSCAN with a similarity threshold (8% error margin means 92% similarity)
clustering = DBSCAN(eps=0.92, min_samples=2, metric='precomputed')

# Convert similarity matrix to distance matrix (1 - similarity)
distance_matrix = 1 - similarity_matrix
labels = clustering.fit_predict(distance_matrix)

print('Images matrix compared')

print('Grouping images..')

# Organize files into groups
clusters = {}
for idx, label in enumerate(labels):
    if label not in clusters:
        clusters[label] = []
    clusters[label].append(filenames[idx])

output_folder = 'output_groups'
os.makedirs(output_folder, exist_ok=True)

for cluster_id, files in clusters.items():
    cluster_folder = os.path.join(output_folder, f'cluster_{cluster_id}')
    print(cluster_folder)
    os.makedirs(cluster_folder, exist_ok=True)
    for file in files:
        filename = os.path.basename(file)
        destination = os.path.join(cluster_folder, filename)
        cv2.imwrite(destination, cv2.imread(file))

print('Images gruped')
