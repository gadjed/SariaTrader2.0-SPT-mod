# Saria Trader 2.0

**SPT 4.0 Compatible**

Server mod that adds the custom trader **Saria** with a rebalanced loyalty-level assort (meds, ammo, grenades, weapons, gear).

Fork of [nameless / KillerDJLang — Saria 4.x.x](https://github.com/KillerDJLang/Saria-4.x.x) with a rewritten shop progression and pricing.

Developed and tested against **SPT 4.0**.

[Latest release](https://github.com/gadjed/SariaTrader2.0-SPT-mod/releases/latest) · [License: MIT](LICENSE)

## Features

- Custom trader **Saria** with avatar and locales
- Loyalty levels **LL1 → LL4** assort: meds, grenades, ammo, optics, magazines, weapon/gear presets
- Optional random stock counts
- Optional removal of money / player-level loyalty requirements
- No client-side plugin required

## What's different in 2.0

- Full assort rewrite focused on progressive PvE usefulness (early survival → late-game crown)
- Prices standardized in **roubles**
- Default config: money LL requirements removed (`RemoveMoneyLlRequirements: true`)
- See [ASSORT.md](ASSORT.md) for the full item/price list

## Install

1. Download `SariaTrader2.0-*.zip` from [Releases](https://github.com/gadjed/SariaTrader2.0-SPT-mod/releases)
2. Extract the archive into your **SPT game root** (the folder that contains `SPT.Server.exe` / `user/`)
3. Restart the SPT server

The zip already contains the correct paths:

```text
user/mods/Saria/nameless-saria.dll
user/mods/Saria/config.json
user/mods/Saria/Assets/...
```

On startup the server log should show:

```text
[Saria] Mission accomplished, returning to base.
```

> If you previously used the original Saria mod, replace `user/mods/Saria/` with this build (same folder name).

## Config

Edit `user/mods/Saria/config.json`:

```json
{
  "RandomizeStockCount": true,
  "RemoveMoneyLlRequirements": true,
  "RemoveLevelLlRequirements": false
}
```

| Key | Description |
|-----|-------------|
| `RandomizeStockCount` | Randomize offer stock amounts on assort generation |
| `RemoveMoneyLlRequirements` | Set all loyalty `MinSalesSum` to `0` |
| `RemoveLevelLlRequirements` | Set all loyalty `MinLevel` to `1` |

## Build from source

Requires **.NET 9** SDK.

```bash
dotnet build nameless-saria.csproj -c Release
```

Output is copied to `Build/SPT/user/mods/Saria/` (dll + config + Assets).

## Credits

- Original mod: **nameless** ([Saria-4.x.x](https://github.com/KillerDJLang/Saria-4.x.x))
- 2.0 assort / packaging: **gadjed**

## License

MIT — see [LICENSE](LICENSE).
Original copyright retained for nameless; modifications copyright gadjed.
