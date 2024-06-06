# Example file showing a basic pygame "game loop"
import pygame
import math
import xml.etree.ElementTree as ET

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

def getCarets(pageNode):
    return [(child.tag, child.text, child.attrib) for child in pageNode.find("story")]

def getOptionArguments(optionNode):
    args = {}

    if optionNode.get("command") == "GOTO_PAGE":
        args["page"] = optionNode.find("page")
    
    return args

def getOptions(pageNode):
    return [(child.get("key"), child.text, child.get("command"), getOptionArguments(child)) for child in pageNode.iter("option")]
        
def getPage(pageNode):
    return (pageNode.get("idx"), getCarets(pageNode), getOptions(pageNode))

def loadBook(name):
    tree = ET.parse("./books/" + name + ".xml")
    root = tree.getroot()
    return [getPage(child) for child in root]

currentPage = 0
pages = loadBook(DEFAULT_BOOK)
currentState = INTRO_STATE

while running:
    # poll for events
    # pygame.QUIT event means the user clicked X to close your window
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
        if event.type == pygame.KEYUP:
            if event.key == pygame.K_F1:
                loadBook(DEFAULT_BOOK)

            if event.key == pygame.K_F12:
                running = False
                break

            options = intropage[2]

            if (currentState == GAME_STATE):
                options = pages[currentPage-1][2]
        
            for option in options:
                if event.key == pygame.key.key_code(option[0]):   
                    command = option[2]                
                    if command == START_GAME:
                        currentState = GAME_STATE
                        currentPage = 1

                    if command == QUIT_GAME:
                        running = False
                        break

                    if command == NEXT_PAGE:
                        if (currentPage != len(pages)-1):
                            currentPage = currentPage+1

                    if command == BACK_PAGE:
                        if (currentPage != 0):
                            currentPage = currentPage-1

                    if command == GOTO_PAGE:
                        if ("page" in command[3]):
                            currentPage = int(command[3]["page"])

                    waitForOption = False

    # fill the screen with a color to wipe away anything from last frame
    screen.fill("black")

    # RENDER YOUR GAME HERE
    pygame.font.init() # you have to call this at the start, 
                   # if you want to use this module.

    if currentState == INTRO_STATE:
        printPage(intropage)
    else:
        printPage(pages[currentPage-1])

    # flip() the display to put your work on screen
    pygame.display.flip()

    waitForOption = True


pygame.quit()

