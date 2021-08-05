using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

[CreateAssetMenu(menuName = "VinTools/Custom Tiles/Advanced Rule Tile")]
public class AdvancedRuleTile : RuleTile<AdvancedRuleTile.Neighbor> {
    [Header("Advanced Tile")]
    [Tooltip("If enabled, the tile will connect to these tiles too when the mode is set to \"This\"")]
    public bool alwaysConnect;
    [Tooltip("Tiles to connect to")]
    public TileBase[] tilesToConnect;
    [Space]
    [Tooltip("Check itseft when the mode is set to \"any\"")]
    public bool checkSelf = true;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Any = 3;
        public const int Specified = 4;
        public const int Nothing = 5;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.This: return Check_This(tile);
            case Neighbor.NotThis: return Check_NotThis(tile);
            case Neighbor.Any: return Check_Any(tile);
            case Neighbor.Specified: return Check_Specified(tile);
            case Neighbor.Nothing: return Check_Nothing(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }

    /// <summary>
    /// Returns true if the tile is this, or if the tile is one of the tiles specified if always connect is enabled.
    /// </summary>
    /// <param name="tile">Neighboring tile to compare to</param>
    /// <returns></returns>
    bool Check_This(TileBase tile)
    {
        if (!alwaysConnect) return tile == this;
        else return tilesToConnect.Contains(tile) || tile == this;

        //.Contains requires "using System.Linq;"
    }

    /// <summary>
    /// Returns true if the tile is not this.
    /// </summary>
    /// <param name="tile">Neighboring tile to compare to</param>
    /// <returns></returns>
    bool Check_NotThis(TileBase tile)
    {
        if (!alwaysConnect) return tile != this;
        else return !tilesToConnect.Contains(tile) && tile != this;

        //.Contains requires "using System.Linq;"
    }

    /// <summary>
    /// Return true if the tile is not empty, or not this if the check self option is disabled.
    /// </summary>
    /// <param name="tile">Neighboring tile to compare to</param>
    /// <returns></returns>
    bool Check_Any(TileBase tile)
    {
        if (checkSelf) return tile != null;
        else return tile != null && tile != this;
    }

    /// <summary>
    /// Returns true if the tile is one of the specified tiles.
    /// </summary>
    /// <param name="tile">Neighboring tile to compare to</param>
    /// <returns></returns>
    bool Check_Specified(TileBase tile)
    {
        return tilesToConnect.Contains(tile);

        //.Contains requires "using System.Linq;"
    }

    /// <summary>
    /// Returns true if the tile is empty.
    /// </summary>
    /// <param name="tile">Neighboring tile to compare to</param>
    /// <param name="tile"></param>
    /// <returns></returns>
    bool Check_Nothing(TileBase tile)
    {
        return tile == null;
    }
}