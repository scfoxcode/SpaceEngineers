import math

# ALL NUMBERS ARE FOR LARGE SHIPS!!!
MAX_ENGINES = 100 # Safety limit

# container for a thruster
class thruster():
    def __init__(self, name, thrust, weight):
        self.name = name
        self.thrust = thrust
        self.weight = weight

# f=ma
def forceRequired(weight, acceleration):
    return weight*acceleration

# How much thrust all engines produce
def thrust(numEngines, force):
    return numEngines*force

# Returns percentage of thrust we have for desired acceleration
def percentOfNeeded(weight, thrust, acceleration):
    required = forceRequired(weight, acceleration)
    return thrust/required*100

SMALL_SHIP_THRUSTERS = (thruster("Small Ion", 12000, 93),
                        thruster("Large Ion", 144000, 721),
                        thruster("Small Hydrogen", 82000, 334),
                        thruster("Large Hydrogen", 400000, 1222),
                        thruster("Small Atmospheric", 80000, 539),
                        thruster("Large Atmospheric", 408000, 4244))


LARGE_SHIP_THRUSTERS = (thruster("Small Ion", 288000, 4380),
                        thruster("Large Ion", 3600000, 43200),
                        thruster("Small Hydrogen", 900000, 1420),
                        thruster("Large Hydrogen", 6000000, 6940),
                        thruster("Small Atmospheric", 420000, 4072),
                        thruster("Large Atmospheric", 5400000, 33834))

grid      = input("Please specify grid size (small:large): ")
THRUSTERS = SMALL_SHIP_THRUSTERS if grid == "small" else LARGE_SHIP_THRUSTERS
weight    = int(input("Please input the weight(Kg) of your ship: "))
gravity   = int(input("Please enter the gravity(ms^-2) you need to escape:"))


escapeForce = forceRequired(weight, gravity)

for t in range(0, len(THRUSTERS)):
    engine = THRUSTERS[t]
    totalThrusters = (gravity*weight)/(engine.thrust - gravity*engine.weight)
    totalThrusters = math.ceil(totalThrusters)
    percentThrust = (((totalThrusters*engine.thrust)/(weight + totalThrusters*engine.weight))/gravity)*100

    print(str(totalThrusters) + " \tx " + engine.name + ":\t " + str(int(percentThrust)) + "%")
