import { defineWorld } from "@latticexyz/world";

export default defineWorld({
  namespace: "nethmudtest",
  tables: {
    Counter: {
      schema: {
        value: "uint32",
      },
      key: [],
    },
    Item: {
      key: ["id"],
      schema: {
        id: "uint32",
        value: "uint32",
      },
    },
  },
});
