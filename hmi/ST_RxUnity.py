import ctypes

class ST_RxUnity(ctypes.Structure):
    _fields_ = [
        ('i', ctypes.c_uint32),
        ('f', ctypes.c_float),
        ('d', ctypes.c_double),
        ('b', ctypes.c_bool)
    ]





