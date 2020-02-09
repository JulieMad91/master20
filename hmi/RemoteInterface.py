from PyQt5 import QtWidgets
from PyQt5 import QtCore
import pyqtgraph as pg
import numpy as np
import socket
from ctypes import *
import time

from MainWindow import Ui_MainWindow
from RealTimePlot import RealTimePlot

# Unity Rx/Tx data types
from ST_RxUnity import ST_RxUnity
from ST_TxUnity import ST_TxUnity

# PLC Rx/Tx dta types


class RemoteInterface(QtWidgets.QMainWindow, Ui_MainWindow):
    bStart = False

    def __init__(self):
        super(RemoteInterface, self).__init__()
        self.gui = Ui_MainWindow()
        self.gui.setupUi(self)

        # Unity socket
        self.unity = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        self.unity.bind(('127.0.0.1', 25000))
        self.unity.setblocking(False)

        # # PLC socket
        # self.plc = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        # self.plc.bind(('192.168.90.60', 50060))

        # Unity UDP data types
        self.txUnity = ST_TxUnity()
        self.rxUnity = ST_RxUnity()

        # Connect buttons
        self.gui.btnStart.clicked.connect(self.start)
        self.gui.btnStop.clicked.connect(self.stop)
        
        
        # Udp Read/Write thread
        self.timer = QtCore.QTimer()
        self.timer.timeout.connect(self.update)
        self.timer.start(50)

        # Initial time
        self.t0 = time.time()

        # Start GUI
        self.show()

    def start(self):
        self.bStart = True

    def stop(self):
        self.bStart = False

    def update(self):
        if not self.bStart:
            self.t0 = time.time()

        # Elapsed time
        t = time.time() - self.t0

        try:
            # Read data from unity
            data = self.unity.recv(1024) 
            memmove(addressof(self.rxUnity), data, sizeof(self.rxUnity))

            # print(data)
            # print('rxUnity.i', self.rxUnity.i)
            # print('rxUnity.f', self.rxUnity.f)
            # print('rxUnity.d', self.rxUnity.d)
            # print('rxUnity.b', self.rxUnity.b)


        except socket.error:
            print(int(t*1000))


        # Populate unity data
        self.txUnity.heave = np.sin(0.2*2*np.pi*t)
        self.txUnity.roll = 5/180*np.pi*np.sin(0.1*2*np.pi*t + np.pi/3)
        self.txUnity.pitch = 5/180*np.pi*np.sin(0.14*2*np.pi*t + np.pi/5)

        # Send data to unity
        self.unity.sendto(self.txUnity, ('127.0.0.1', 25001))
        
             




    def closeEvent(self, event):
        self.timer.stop()
