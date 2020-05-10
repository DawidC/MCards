import pandas as pd
import requests
import numpy as np


file_name = "input.xlsx"
dfs = pd.read_excel(file_name, sheet_name="Sheet1")

dfslist = [dfs.to_dict(orient='index')]

print(dfslist)

for it in range(len(dfslist[0])):
    edition = dfslist[0][it]['DODATEK']
    cardNumber = dfslist[0][it]['NUMER']
    isFoil = dfslist[0][it]['FOIL']

    url = 'https://api.scryfall.com/cards/'+edition.lower()+'/'+str(cardNumber)
    resp = requests.get(url=url)
    data = resp.json()
    name = ''
    mana_cost = ''
    current_market_price_usd = ''
    legal_standard = ''
    type_line = ''
    rarity = ''

    if data['object'] != 'error':
        name = data['name']
        if data['layout'] == 'normal':
            mana_cost = data['mana_cost']
        # CHECK IF FOIL
        if isFoil == 1:
            current_market_price_usd = data['prices']['usd_foil']
        else:
            current_market_price_usd = data['prices']['usd']
        legal_standard = data['legalities']['standard']
        type_line = data['type_line']
        rarity = data['rarity']

    else:
        print(edition)
        print(cardNumber)
        print(isFoil)
        name = 'not_found'
        mana_cost = 'x'
        current_market_price_usd = 'x'
        legal_standard = 'x'
        type_line = 'x'
        rarity = 'x'

    dfslist[0][it]['name'] = name
    dfslist[0][it]['mana_cost'] = mana_cost
    dfslist[0][it]['current_market_price'] = current_market_price_usd
    dfslist[0][it]['legal_in_standard'] = legal_standard
    dfslist[0][it]['type_line'] = type_line
    dfslist[0][it]['type'] = rarity


    # print(data)
    # print(dfslist[0][it])
print(dfslist)
out_dfs = pd.DataFrame.from_dict(dfslist[0], orient='index')
out_dfs.to_excel("input.xlsx", index=False)
print(out_dfs)


