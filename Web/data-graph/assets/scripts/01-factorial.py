import math

def get_dataset(x):
    factorial = math.factorial(x)
    increment = 1

    if(factorial >= 40320):
        increment = -1

    return [ {"x": x, "factorial": factorial, "increment": increment } ]

