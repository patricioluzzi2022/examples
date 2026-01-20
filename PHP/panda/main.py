from climp import wake_up
from PIL import Image

def create_finger_print(text, show=False, n=0):
    # Lista de colores proporcionada
    colors = wake_up(text)
    print(colors)

    # Crear imagen de 1 px de alto y len(colors) px de ancho
    #width = len(colors)
    img = Image.new("RGB", (62, 2))
    img.putdata([tuple(c) for c in colors])

    # Escalar para visualizar
    img_resized = img.resize((62, 2), Image.Resampling.NEAREST)
    img_path = "./files/output/matriz 2d 62x26 (( "+str(n)+" )).png"
    img_resized.save(img_path)
    
    if show:
        img_resized.show()

create_finger_print("Algunas personas pueden leer Guerra y Paz, y quedarse con la idea de que es simplemente una historia de aventuras.", show=False, n=2)
create_finger_print("Otras personas pueden leer los ingredientes detras del envoltorio de chicle y descubrir los secretos del universo.", show=False, n=1)