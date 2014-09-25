public class Level {

	private static int levelNum;

	private string _Name;
	private int _TimeLimit, _LevelNum;
	private int[] _TroopTypes, _TowerTypes;

	public int    LevelNum  { get; set; }
	public string Name      { get; set; }
	public int 	  TimeLimit { get; set; }

	public Level (string name, int timeLimit, int[] troopTypes, int[] towerTypes) {
		 
		this._LevelNum   = ++Level.levelNum;
		this._Name       = name;
		this._TimeLimit  = timeLimit;
		this._TroopTypes = troopTypes;
		this._TowerTypes = towerTypes; 
	
	}
	
}