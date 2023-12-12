from flask import Flask,request,render_template,redirect
import sqlite3
import os

class MonitorRec(object):
    def __init__(self,timestamp,positivechecks,negativechecks):
        self.timestamp = timestamp
        self.positivechecks = positivechecks
        self.negativechecks = negativechecks

DATA_PATH = "/database"

if os.path.exists(DATA_PATH) == False:
    os.mkdir(DATA_PATH)

app = Flask(__name__)

def init_tables():

    CREATE_TABLE = "CREATE TABLE IF NOT EXISTS uptime_monitor (timestamp TEXT, positivechecks TEXT, negativechecks TEXT)"
    conn = sqlite3.connect(os.path.join(DATA_PATH,"monitorDB.db"))
    cursor = conn.cursor()
    cursor.execute(CREATE_TABLE)
    conn.commit()
    conn.close()

init_tables()

def add_monitoring(timestamp,positivechecks,negativechecks):

    ADD_MONITORING = "INSERT INTO uptime_monitor (timestamp,positivechecks,negativechecks) VALUES (?,?,?)"
    conn = sqlite3.connect(os.path.join(DATA_PATH,"monitorDB.db"))
    cursor = conn.cursor()
    conn.execute(ADD_MONITORING,(timestamp,positivechecks,negativechecks))
    conn.commit()
    conn.close()

def load_monitorings():

    monitorings = []

    LOAD_MONITORINGS = "SELECT * from uptime_monitor"
    conn = sqlite3.connect(os.path.join(DATA_PATH,"monitorDB.db"))
    cursor = conn.cursor()
    results = conn.execute(LOAD_MONITORINGS)

    for row in results:
        monitorings.append(MonitorRec(row[0],row[1],row[2]))
    return monitorings

@app.route("/",methods=["POST","GET"])
def index():

    if request.method == "POST":
        timestamp = request.form["timestamp"]
        positivechecks = request.form["positivechecks"]
        negativechecks = request.form["negativechecks"]

        add_monitoring(timestamp,positivechecks,negativechecks)

        return redirect("/list")

    return render_template("index.html")

@app.route("/list",methods=["GET"])
def list():

    monitorings = load_monitorings()

    return render_template("list.html",monitorings=monitorings)

app.run(host="0.0.0.0",port=5000)
