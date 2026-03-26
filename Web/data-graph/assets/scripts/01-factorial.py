import math

def get_dataset(params):
    if params["sign"] == 1:
        if 0 <= params["x"] < 6:
            sign = 1
        else:
            sign = -1
    else:
        if 0 < params["x"] <= 6:
            sign = -1
        else:
            sign = 1

    factorial = math.factorial(params["x"] + sign)

    params["factorial"] = factorial + 440
    print(params)

    response = [{ 
        "variables": params["variables"],
        "x": params["x"],
        "result": factorial + math.tan(params["x"]*math.pi / 4),
        "direction": sign,
        "script": {
            "id": 1,
            "file": "assets/scripts/01-factorial.py"
        }
    }]

    
    
    return response
