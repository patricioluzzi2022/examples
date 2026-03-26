import math

def get_dataset(params):
    combination = math.comb(params["n"], params["k"])
    sign = 1

    if(combination >= 40320):
        sign = -1

    return [ { "variables": params["variables"], "n": params["n"], "k": params["k"], "result": combination, "direction": sign, "script": { "id": 2, "file": "assets/scripts/02-combination.py" } } ]

