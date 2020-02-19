import ctypes

class ST_TxUnity(ctypes.Structure):
    _fields_ = [        
        ('heave', ctypes.c_float),
        ('roll', ctypes.c_float),
        ('pitch', ctypes.c_float)
    ]





