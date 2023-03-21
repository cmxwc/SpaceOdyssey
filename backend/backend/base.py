import json
from deta import Deta


class database():
    def __init__(self, env):
        if env == 'deta':
            self.deta = Deta()
            print("Running Deta Server")
        else:
            self.deta = False
            self.folder = "data/"
            if env == "TEST":
                self.folder = "data/test/"
                print("Running Test...")
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
        if self.deta:
            users = self.deta.Base(name)
            users.put(data)
        else:
            with open(f"{self.folder}{name}.json", "w") as f:
                json.dump(data, f)

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
    def delete_json(self, name, key):
        if self.deta:
            users = self.deta.Base(name)
            users.delete(key)
        else:
            print("nvm")
    def put_many_json(self, name, data):
        if self.deta:
            users = self.deta.Base(name)
            users.put_many(data)

