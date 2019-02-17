namespace Customcommand
{
    public class Patcher
    {
        public static void Patch()
        {
            try
            {
                WaterParkCreature.waterParkCreatureParameters[TechType.ReaperLeviathan] = new WaterParkCreatureParameters(0.01f, 0.05f, 1f, 3f, true);
                WaterParkCreature.waterParkCreatureParameters[TechType.SeaDragon] = new WaterParkCreatureParameters(0.01f, 0.05f, 1f, 3f, true);
                WaterParkCreature.waterParkCreatureParameters[TechType.GhostLeviathan] = new WaterParkCreatureParameters(0.01f, 0.05f, 1f, 3f, true);
                WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperorJuvenile] = new WaterParkCreatureParameters(0.01f, 0.05f, 1f, 3f, true);
                WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperorBaby] = new WaterParkCreatureParameters(0.1f, 0.3f, 1f, 3f, true);
                WaterParkCreature.waterParkCreatureParameters[TechType.GhostLeviathanJuvenile] = new WaterParkCreatureParameters(0.01f, 0.05f, 1f, 3f, true);
                WaterParkCreature.waterParkCreatureParameters[TechType.Warper] = new WaterParkCreatureParameters(0.05f, 0.2f, 1f, 3f, true);
            }
            catch
            {}
            strat.Awake();
        }
    }
}
