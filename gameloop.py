# Example file showing a basic pygame "game loop"
import pygame
import math
import random
import xml.etree.ElementTree as ET
import json


# pygame setup
pygame.init()
screen = pygame.display.set_mode((1280, 720))
clock = pygame.time.Clock()
running = True

DEFAULT_BOOK = "Warlock_Of_Firetop_Mountain"


HEADER_FONT_SIZE = 16
NORMAL_FONT_SIZE = 13
DEFAULT_TEXT_FONT = 'Comic Sans MS'
DEFAULT_TEXT_BACKGROUND_COLOR = "#101010"
DEFAULT_TEXT_COLOR = "#FFFFFF"
RED_COLOR = "#FF0000"
BLACK_COLOR = "#000000"

MAX_OPTION_COLS = 3

TEXT_TOP = 20
TEXT_LEFT = 20

OPTIONS_TOP = 620
OPTIONS_LEFT = 50

INTRO_STATE = 1
GAME_STATE = 2

START_GAME = "START_GAME"
QUIT_GAME = "QUIT_GAME"
NEXT_PAGE = "NEXT_PAGE"
BACK_PAGE = "BACK_PAGE"
GOTO_PAGE = "GOTO_PAGE"
TEST_LUCK = "TEST_LUCK"
FIGHT_MONSTERS = "FIGHT_MONSTERS"


intropage = ("0",
    [("text", "The Warlock Of Firetop Mountain\n\n", {"text-color": DEFAULT_TEXT_COLOR, "background-color": RED_COLOR, "font-size": HEADER_FONT_SIZE}),
    ("text", "Welcome to my digital interpretation \n of the Fighting Fantasy book", {}),
    ("text", "The Warlock Of Firetop Mountain", {"text-color": RED_COLOR}),
    ("text", "\n by Steven Jackson and Ian Livingstone", {})],
    [(pygame.key.name(pygame.K_s), "Start", START_GAME),
     (pygame.key.name(pygame.K_q), "Quit", QUIT_GAME)]
)

def getLineSpacing(fontSize):
    return math.floor(fontSize+fontSize/2)

def applyFormatting(text):
    return text.replace('\t', "     ")

def printPage(page):
    charpos = TEXT_LEFT
    linepos = TEXT_TOP
    for caret in page[1]:
        if (caret[0] == "text"):
            font = DEFAULT_TEXT_FONT
            fontSize = NORMAL_FONT_SIZE
            textColor = DEFAULT_TEXT_COLOR
            backgroundColor = DEFAULT_TEXT_BACKGROUND_COLOR

            if("font" in caret[2]):
                font = caret[2]["font"]

            if("font-size" in caret[2]):
                fontSize = int(caret[2]["font-size"])

            if("text-color" in caret[2]):
                textColor = caret[2]["text-color"]

            if("background-color" in caret[2]):
                backgroundColor = caret[2]["background-color"]
            
            text_font = pygame.font.SysFont(font, fontSize)

            endOfText = False
            newLineIdx = 0
            text = caret[1]

            lineCarets = []
            

            while not endOfText:
                nextNewLineIdx = text.find('\n', newLineIdx)
                if (nextNewLineIdx >= 0):
                    subString = text[newLineIdx:nextNewLineIdx]

                    if (subString.strip() != ""):
                        lineCarets.append(applyFormatting(subString))

                    lineCarets.append('\n')

                    newLineIdx = nextNewLineIdx+1
                    
                    if (nextNewLineIdx >= len(text)-1):
                        endOfText = True
                else:
                    lineCarets.append(applyFormatting(text[newLineIdx:]))
                    endOfText = True

            for line in lineCarets:
                lineText = line.strip()
                charOffset = 0
                if(lineText != ""):
                    text_surface = text_font.render(line, False, pygame.Color(textColor), pygame.Color(backgroundColor))
                    screen.blit(text_surface, (charpos, linepos))
                    charOffset = text_surface.get_rect().bottomright[0]
                
                if (line.endswith('\n')):
                    linepos = linepos + getLineSpacing(fontSize)
                    charpos = TEXT_LEFT
                else:
                    charpos = charpos + charOffset

    colNum = 1
    charpos = OPTIONS_LEFT
    linepos = OPTIONS_TOP
    options_font = pygame.font.SysFont('Comic Sans MS', NORMAL_FONT_SIZE)

    for option in page[2]:
        text = option[0] + " - " + option[1]

        text_surface = options_font.render(text, False, DEFAULT_TEXT_COLOR, DEFAULT_TEXT_BACKGROUND_COLOR)
        screen.blit(text_surface, (charpos, linepos))

        if colNum == MAX_OPTION_COLS:
            charpos = 100
            colNum = 1
            linepos = linepos + getLineSpacing(NORMAL_FONT_SIZE)
        else:
            charpos = charpos + 400
            colNum = colNum + 1

def appendStory(page, carets):
    for caret in carets:
        page[1].append(caret)

def setOptions(page, options):    
    page[2].clear()
    for option in options: 
        page[2].append(option)

def appendTextToStory(page, text):
    appendStory(page, [("text", text, {})])

def getResult(resultNode):
    carets = [(child.tag, child.text, child.attrib) for child in resultNode.find("story")]
    options = [(child.get("key"),  getOptionLabel(child), child.get("command"), getOptionArguments(child)) for child in resultNode if child.tag == "option"]

    return (carets, options)


def getOptionArguments(optionNode):
    args = {}

    if optionNode.get("command") == "GOTO_PAGE":
        args["page"] = optionNode.get("page")
        
    if optionNode.get("command") == "TEST_LUCK":
        args["pass"] = getResult(optionNode.find("pass"))
        args["fail"] = getResult(optionNode.find("fail"))
    
    return args

def getOptionLabel(optionNode):
    if optionNode.find("label") is not None:
        return optionNode.find("label").text
    
    return ""
    
def getPage(pageNode):
    carets = [(child.tag, child.text, child.attrib) for child in pageNode.find("story")]
    options = [(child.get("key"), getOptionLabel(child), child.get("command"), getOptionArguments(child)) for child in pageNode if child.tag == "option"]
    
    return (pageNode.get("idx"), carets, options)

def loadBook(name):
    tree = ET.parse("./books/" + name + ".xml")
    root = tree.getroot()
    return ([getPage(child) for child in root if child.tag == "intropage"], [getPage(child) for child in root if child.tag == "gamepage"])

book = loadBook(DEFAULT_BOOK)
intropages = book[0]
gamepages = book[1]

currentGameState = INTRO_STATE
currentPage = 0

luckscore = 6

def getCurrentState():
    return {"state": currentGameState, "page": currentPage, "luck": luckscore }

def getSaveState():
    with open('savegame.json', 'r') as save_file:
        return json.load(save_file)
    
def resetGameState(state):
    book = loadBook(DEFAULT_BOOK)
    intropages = book[0]
    gamepages = book[1]
    currentGameState = INTRO_STATE
    currentPage = 1
    luckscore = 6

def setSaveState(state):
    with open('savegame.json', 'w') as save_file:
        save_file.write(json.dumps(state))

def testLuck(page):
    diceCheck = random.randint(1, 6) + random.randint(1, 6)
    appendTextToStory(page, "Luck: " + str(luckscore) + "\n")
    appendTextToStory(page, "Roll: " + str(diceCheck) + "\n")
    return diceCheck <= luckscore

while running:
    # poll for events
    # pygame.QUIT event means the user clicked X to close your window
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
        if event.type == pygame.KEYUP:
            if event.key == pygame.K_F12:
                running = False
                break
            
            if event.key == pygame.K_F11:
                state = getCurrentState()
                setSaveState(state)
                break

            if event.key == pygame.K_F10:
                state = getSaveState()
                if "state" in state: currentGameState = state["state"]
                if "page" in state: currentPage = state["page"]
                if "luck" in state: luckscore = int(state["luck"])
                break

            if (currentPage == 0):
                options = intropage[2]
            elif (currentGameState == INTRO_STATE):
                options = intropages[currentPage-1][2]
            elif (currentGameState == GAME_STATE):
                options = gamepages[currentPage-1][2]
        
            for option in options:
                if event.key == pygame.key.key_code(option[0]):   
                    command = option[2]                
                    if command == START_GAME:
                        book = loadBook(DEFAULT_BOOK)
                        intropages = book[0]
                        gamepages = book[1]
                        currentGameState = INTRO_STATE
                        currentPage = 1
                        luckscore = 6
                        break

                    if command == QUIT_GAME:
                        running = False
                        break

                    if command == NEXT_PAGE:
                        if (currentGameState == INTRO_STATE):
                            if currentPage < len(intropages):
                                currentPage = currentPage+1
                            else:
                                currentPage = 1
                                currentGameState = GAME_STATE
                        elif (currentGameState == GAME_STATE and currentPage < len(gamepages)):
                            currentPage = currentPage+1

                    if command == BACK_PAGE:
                        if (currentPage != 0):
                            currentPage = currentPage-1

                    if command == GOTO_PAGE:
                        if ("page" in option[3]):
                            currentPage = int(option[3]["page"])
                            
                    if command == TEST_LUCK:
                        if testLuck(gamepages[currentPage-1]):
                            appendStory(gamepages[currentPage-1], option[3]["pass"][0])
                            setOptions(gamepages[currentPage-1], option[3]["pass"][1])
                        else:
                            appendStory(gamepages[currentPage-1], option[3]["fail"][0])
                            setOptions(gamepages[currentPage-1], option[3]["fail"][1])  

                    if command == FIGHT_MONSTERS:
                        if resolveBattle(battleState):
                            appendStory(gamepages[currentPage-1], option[3]["win"][0])
                            setOptions(gamepages[currentPage-1], option[3]["win"][1])
                        else:
                            appendStory(gamepages[currentPage-1], option[3]["defeat"][0])
                            setOptions(gamepages[currentPage-1], option[3]["defeat"][1])  

                    break

    # fill the screen with a color to wipe away anything from last frame
    screen.fill("black")

    # RENDER YOUR GAME HERE
    pygame.font.init() # you have to call this at the start, 
                   # if you want to use this module.

    if currentPage == 0:
        printPage(intropage)
    else:
        if currentGameState == INTRO_STATE:
            printPage(intropages[currentPage-1])
        else:
            printPage(gamepages[currentPage-1])

    # flip() the display to put your work on screen
    pygame.display.flip()


pygame.quit()

