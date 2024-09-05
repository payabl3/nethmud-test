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
  },
});
