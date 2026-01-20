import math

def to_ascii(c):
    return ord(c)

def numero_amigo(n):
    if not (0 <= n <= 255):
        raise ValueError("El número debe estar en el rango 0 a 255.")

    # Devuelve la suma de los divisores propios de n (excluyendo n)
    amigo = sum(i for i in range(1, n) if n % i == 0)

    # El posible número amigo debe estar también en el rango y si no esta, no hay drama
    if amigo > 255:
        amigo_virtual = 255 - (amigo - 255)
        return amigo_virtual

    return amigo

def climp(n1, n2):
    if not (isinstance(n1, int) and isinstance(n2, int)):
        raise TypeError("Ambos catetos deben ser enteros.")
    if n1 < 0 or n2 < 0:
        raise ValueError("Los catetos no pueden ser negativos.")
    
    h = math.sqrt(n1**2 + n2**2)
    h_entero = round(h)

    if 0 <= h_entero <= 255:
        return h_entero
    else:
        raise ValueError(f"Hipotenusa fuera de rango: {h_entero} (esperado 0–255)")

def wake_up(texto):
    ascii_arr = []
    for c in texto:
        valor = to_ascii(c)
        if 0 <= valor <= 255:
            ascii_arr.append(valor)
        else:
            raise ValueError(f"Caracter fuera de rango ASCII extendido: '{c}' (valor: {valor})")

    pixel_arr = []

    for n in ascii_arr:
        r = n
        g = numero_amigo(n)
        b = climp(r, g)
        pixel_arr.append([r, g, b])

    return pixel_arr

#print(wake_up("Los secretos del universo en el envoltorio de un chicle"))