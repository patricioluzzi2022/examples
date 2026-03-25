import math

def get_dataset(n, k):
    combination = math.comb(n, k)
    increment = 1

    if(combination >= 40320):
        increment = -1

    return [ {"n": n, "k": k, "combination": combination, "increment": increment, "script": { "id": 2, "file": "assets/scripts/02-combination.py" } } ]

