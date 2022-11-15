import json
from deta import Deta


class database():
    def __init__(self, env):
        if env == 'deta':
            self.deta = Deta('c0nydj0i_8G5hUausise6e3TjRpWkcrJh2GhoBrbJ')
            print("Running Deta Server")
        else:
            self.deta = False
            self.folder = "data/"
            # if env == "TEST":
            #     self.folder = "data/test/"
            # print("Running NTU Server")

    def save_json(self, name, data):
        if self.deta:
            users = self.deta.Base(name)
            users.put(data)
        else:
            og = self.load_json(name)
            og.append(data)
            with open(f"{self.folder}{name}.json", "w") as f:
                json.dump(og, f)

    def update_json(self, name, data):
        users = self.deta.Base(name)
        users.put(data)

    def load_json(self, name, limit=0):
        if self.deta:
            users = self.deta.Base(name)
            fetch_res = users.fetch().items
            for i in fetch_res:
                del i['key']
            return fetch_res
        else:
            try:
                with open(f"{self.folder}{name}.json", "r") as f:
                    data = json.load(f)
            except:
                data = []
            return data
