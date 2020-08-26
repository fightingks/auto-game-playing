import time
import os
import sys
import random
import tkinter
from tkinter import Tk,messagebox
import pyautogui
from PIL import Image,ImageGrab,ImageEnhance,ImageTk
from xml.etree.ElementTree import ElementTree
from pynput.mouse import Button, Controller


def find_picture(image):
    #找图
    #定位图片，得到指定坐标（随机）
    location=pyautogui.locateOnScreen(image=image)#判定目标截图在系统上的位置
        
        #获取目标图像在系统中的随机坐标位置，生成点击坐标
    x,y=pyautogui.center(location)


    return x,y


    
def mouse_click(x,y):
    #对识别出的目标图像进行点击
    if x!=-1:
        pyautogui.moveTo(x,y,duration=0.5)
        pyautogui.click(x=x,y=y,clicks=1,button='left')
        time.sleep(1+random.uniform(-1, 1)*1)
    else:
        messagebox.showinfo("结果","找不到图片！")
def pictureclick(name):
    base=os.getcwd()
    path=os.path.join(os.getcwd()+'\picture',name)
    print(path)
    x,y=find_picture(path)
    mouse_click(x,y)

def IF(iffind,val,ifval):
    base=os.getcwd()
    path=os.path.join(os.getcwd()+'\picture',val)
    x,y=find_picture(path)
    if iffind=="找到" and x==-1:
        pictureclick(ifval);
    elif iffind=="没找到" and x!=-1:
        pictureclick(ifval);
    else:
        pass   

def readpath():
    tree = ElementTree(file=os.path.dirname(os.path.realpath(__file__))+"\\name.xml")
    root = tree.getroot()
    name= root.find('name')
    name=name.text
    return name

def readXML():
    global func,text
    tree = ElementTree(file=readpath())
    root = tree.getroot()
    children= root.getchildren()
    for child in children:
        func=child.get('function')
        val=child.text.replace(" ","")
        ifval=child.get('if_value').replace(" ","")
        iffind=child.get('if_find').replace(" ","")
        if func=="pictureclick":
            pictureclick(val)
        if func=="IF":
            IF(iffind,val,ifval)
window=tkinter.Tk()
window.withdraw()
pyautogui.FAILSAFE = False
readXML()
