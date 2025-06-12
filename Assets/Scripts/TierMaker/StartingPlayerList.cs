using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartingPlayerList : MonoBehaviour
{
    [HideInInspector] public List<DraftPick> InitialPlayers = new();

    [HideInInspector] public static bool IsDynasty;

    private int _playerIdNumberCount = 0;

    private const int YEAR = 2025;
    private const float VERSION = 8.0f;

    public void LoadInitialPlayers()
    {
        if (!PlayerPrefs.HasKey(nameof(YEAR)) || PlayerPrefs.GetInt(nameof(YEAR)) != YEAR || !PlayerPrefs.HasKey(nameof(VERSION)) || PlayerPrefs.GetFloat(nameof(VERSION)) != VERSION)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt(nameof(YEAR), YEAR);
            PlayerPrefs.SetFloat(nameof(VERSION), VERSION);
            PlayerPrefs.Save();
        }

        CreatePlayer("Ja'marr Chase", ItemSlot.Position.WR, "CIN", 1, 1);
        CreatePlayer("Saquon Barkley", ItemSlot.Position.RB, "PHI", 2, 10);
        CreatePlayer("Justin Jefferson", ItemSlot.Position.WR, "MIN", 3, 2);
        CreatePlayer("Bijan Robinson", ItemSlot.Position.RB, "ATL", 4, 3);
        CreatePlayer("Ceedee Lamb", ItemSlot.Position.WR, "DAL", 7, 7);
        CreatePlayer("Puka Nacua", ItemSlot.Position.WR, "LAR", 8, 12);
        CreatePlayer("Amon-Ra St. Brown", ItemSlot.Position.WR, "DET", 6, 9);
        CreatePlayer("Jahmyr Gibbs", ItemSlot.Position.RB, "DET", 5, 4);
        CreatePlayer("Nico Collins", ItemSlot.Position.WR, "HOU", 10, 16);
        CreatePlayer("Malik Nabers", ItemSlot.Position.WR, "NYG", 9, 5);
        CreatePlayer("Drake London", ItemSlot.Position.WR, "ATL", 15, 17);
        CreatePlayer("Brian Thomas", ItemSlot.Position.WR, "JAX", 12, 13);
        CreatePlayer("A.J. Brown", ItemSlot.Position.WR, "PHI", 17, 25);
        CreatePlayer("Derrick Henry", ItemSlot.Position.RB, "BAL", 11, 40);
        CreatePlayer("De'von Achane", ItemSlot.Position.RB, "MIA", 16, 18);
        CreatePlayer("Brock Bowers", ItemSlot.Position.TE, "LV", 14, 6);
        CreatePlayer("Tyreek Hill", ItemSlot.Position.WR, "MIA", 27, 60);
        CreatePlayer("Jaxon Smith-Njigba", ItemSlot.Position.WR, "SEA", 24, 26);
        CreatePlayer("Trey McBride", ItemSlot.Position.TE, "ARI", 22, 21);
        CreatePlayer("Ladd McConkey", ItemSlot.Position.WR, "LAC", 21, 19);
        CreatePlayer("Tee Higgins", ItemSlot.Position.WR, "CIN", 27, 39);
        CreatePlayer("Josh Allen", ItemSlot.Position.QB, "BUF", 20, 11);
        CreatePlayer("Bucky Irving", ItemSlot.Position.RB, "TB", 19, 23);
        CreatePlayer("Lamar Jackson", ItemSlot.Position.QB, "BAL", 25, 15);
        CreatePlayer("Garrett Wilson", ItemSlot.Position.WR, "NYJ", 30, 27);
        CreatePlayer("Josh Jacobs", ItemSlot.Position.RB, "GB", 23, 35);
        CreatePlayer("Terry McLaurin", ItemSlot.Position.WR, "WAS", 31, 46);
        CreatePlayer("Christian McCaffrey", ItemSlot.Position.RB, "SF", 18, 34);
        CreatePlayer("Jonathan Taylor", ItemSlot.Position.RB, "IND", 26, 31);
        CreatePlayer("Mike Evans", ItemSlot.Position.WR, "TB", 35, 69);
        CreatePlayer("Jayden Daniels", ItemSlot.Position.QB, "WAS", 29, 14);
        CreatePlayer("Breece Hall", ItemSlot.Position.RB, "NYJ", 34, 29);
        CreatePlayer("Chase Brown", ItemSlot.Position.RB, "CIN", 33, 43);
        CreatePlayer("Davante Adams", ItemSlot.Position.WR, "LAR", 39, 83);
        CreatePlayer("Marvin Harrison", ItemSlot.Position.WR, "ARI", 36, 20);
        CreatePlayer("DJ Moore", ItemSlot.Position.WR, "CHI", 42, 57);
        CreatePlayer("Ashton Jeanty", ItemSlot.Position.RB, "LV", 13, 8);
        CreatePlayer("Kyren Williams", ItemSlot.Position.RB, "LAR", 32, 32);
        CreatePlayer("T.J. Hockenson", ItemSlot.Position.TE, "MIN", 53, 64);
        CreatePlayer("Jalen Hurts", ItemSlot.Position.QB, "PHI", 38, 28);
        CreatePlayer("James Cook", ItemSlot.Position.RB, "BUF", 37, 37);
        CreatePlayer("Kenneth Walker", ItemSlot.Position.RB, "SEA", 44, 48);
        CreatePlayer("Zay Flowers", ItemSlot.Position.WR, "BAL", 51, 51);
        CreatePlayer("Rashee Rice", ItemSlot.Position.WR, "KC", 43, 41);
        CreatePlayer("Courtland Sutton", ItemSlot.Position.WR, "DEN", 49, 85);
        CreatePlayer("Chris Olave", ItemSlot.Position.WR, "NO", 61, 59);
        CreatePlayer("Alvin Kamara", ItemSlot.Position.RB, "NO", 45, 71);
        CreatePlayer("Joe Burrow", ItemSlot.Position.QB, "CIN", 40, 24);
        CreatePlayer("DK Metcalf", ItemSlot.Position.WR, "PIT", 50, 62);
        CreatePlayer("Chuba Hubbard", ItemSlot.Position.RB, "CAR", 48, 55);
        CreatePlayer("Jaylen Waddle", ItemSlot.Position.WR, "MIA", 63, 72);
        CreatePlayer("Joe Mixon", ItemSlot.Position.RB, "HOU", 46, 56);
        CreatePlayer("George Kittle", ItemSlot.Position.TE, "SF", 41, 54);
        CreatePlayer("James Conner", ItemSlot.Position.RB, "ARI", 56, 86);
        CreatePlayer("Devonta Smith", ItemSlot.Position.WR, "PHI", 54, 45);
        CreatePlayer("Chris Godwin", ItemSlot.Position.WR, "TB", 67, 96);
        CreatePlayer("Jordan Addison", ItemSlot.Position.WR, "MIN", 62, 61);
        CreatePlayer("Jameson Williams", ItemSlot.Position.WR, "DET", 59, 68);
        CreatePlayer("George Pickens", ItemSlot.Position.WR, "DAL", 60, 63);
        CreatePlayer("David Montgomery", ItemSlot.Position.RB, "DET", 55, 70);
        CreatePlayer("Baker Mayfield", ItemSlot.Position.QB, "TB", 68, 95);
        CreatePlayer("Omarion Hampton", ItemSlot.Position.RB, "LAC", 47, 22);
        CreatePlayer("Patrick Mahomes", ItemSlot.Position.QB, "KC", 58, 49);
        CreatePlayer("Jerry Jeudy", ItemSlot.Position.WR, "CLE", 71, 76);
        CreatePlayer("Xavier Worthy", ItemSlot.Position.WR, "KC", 57, 47);
        CreatePlayer("Sam LaPorta", ItemSlot.Position.TE, "DET", 52, 38);
        CreatePlayer("Isiah Pacheco", ItemSlot.Position.RB, "KC", 64, 75);
        CreatePlayer("Tyrone Tracy", ItemSlot.Position.RB, "NYG", 78, 79);
        CreatePlayer("Najee Harris", ItemSlot.Position.RB, "LAC", 85, 112);
        CreatePlayer("Aaron Jones", ItemSlot.Position.RB, "MIN", 70, 104);
        CreatePlayer("Tony Pollard", ItemSlot.Position.RB, "TEN", 75, 101);
        CreatePlayer("Brandon Aiyuk", ItemSlot.Position.WR, "SF", 88, 81);
        CreatePlayer("Rome Odunze", ItemSlot.Position.WR, "CHI", 77, 44);
        CreatePlayer("Khalil Shakir", ItemSlot.Position.WR, "BUF", 81, 99);
        CreatePlayer("Jared Goff", ItemSlot.Position.QB, "DET", 91, 138);
        CreatePlayer("Jaylen Warren", ItemSlot.Position.RB, "PIT", 90, 125);
        CreatePlayer("Jonnu Smith", ItemSlot.Position.TE, "MIA", 76, 114);
        CreatePlayer("Mark Andrews", ItemSlot.Position.TE, "BAL", 82, 90);
        CreatePlayer("Kyler Murray", ItemSlot.Position.QB, "ARI", 93, 107);
        CreatePlayer("Justin Herbert", ItemSlot.Position.QB, "LAC", 101, 74);
        CreatePlayer("Jakobi Meyers", ItemSlot.Position.WR, "LV", 86, 119);
        CreatePlayer("Brian Robinson", ItemSlot.Position.RB, "WAS", 83, 89);
        CreatePlayer("Calvin Ridley", ItemSlot.Position.WR, "TEN", 80, 120);
        CreatePlayer("D'Andre Swift", ItemSlot.Position.RB, "CHI", 79, 87);
        CreatePlayer("Travis Kelce", ItemSlot.Position.TE, "KC", 74, 113);
        CreatePlayer("Deebo Samuel", ItemSlot.Position.WR, "WAS", 87, 109);
        CreatePlayer("David Njoku", ItemSlot.Position.TE, "CLE", 97, 108);
        CreatePlayer("Evan Engram", ItemSlot.Position.TE, "DEN", 94, 134);
        CreatePlayer("Travis Etienne", ItemSlot.Position.RB, "JAX", 96, 88);
        CreatePlayer("Jordan Love", ItemSlot.Position.QB, "GB", 111, 103);
        CreatePlayer("Rhamondre Stevenson", ItemSlot.Position.RB, "NE", 106, 126);
        CreatePlayer("Jayden Reed", ItemSlot.Position.WR, "GB", 98, 92);
        CreatePlayer("Bo Nix", ItemSlot.Position.QB, "DEN", 84, 66);
        CreatePlayer("Cooper Kupp", ItemSlot.Position.WR, "SEA", 92, 118);
        CreatePlayer("Brock Purdy", ItemSlot.Position.QB, "SF", 107, 123);
        CreatePlayer("Josh Downs", ItemSlot.Position.WR, "IND", 103, 98);
        CreatePlayer("Michael Pittman", ItemSlot.Position.WR, "IND", 108, 117);
        CreatePlayer("Caleb Williams", ItemSlot.Position.QB, "CHI", 109, 73);
        CreatePlayer("Rico Dowdle", ItemSlot.Position.RB, "CAR", 124, 168);
        CreatePlayer("Stefon Diggs", ItemSlot.Position.WR, "NE", 95, 133);
        CreatePlayer("Tyjae Spears", ItemSlot.Position.RB, "TEN", 117, 129);
        CreatePlayer("Jalen McMillan", ItemSlot.Position.WR, "TB", 138, 110);
        CreatePlayer("Dalton Kincaid", ItemSlot.Position.TE, "BUF", 110, 82);
        CreatePlayer("Jake Ferguson", ItemSlot.Position.TE, "DAL", 115, 128);
        CreatePlayer("Javonte Williams", ItemSlot.Position.RB, "DAL", 104, 137);
        CreatePlayer("Rachaad White", ItemSlot.Position.RB, "TB", 122, 139);
        CreatePlayer("Tucker Kraft", ItemSlot.Position.TE, "GB", 116, 102);
        CreatePlayer("C.J. Stroud", ItemSlot.Position.QB, "HOU", 118, 77);
        CreatePlayer("JK Dobbins", ItemSlot.Position.RB, "DEN", 151, 149);
        CreatePlayer("Darnell Mooney", ItemSlot.Position.WR, "ATL", 113, 142);
        CreatePlayer("Keenan Allen", ItemSlot.Position.WR, "FA", 154, 220);
        CreatePlayer("Ricky Pearsall", ItemSlot.Position.WR, "SF", 99, 91);
        CreatePlayer("Dak Prescott", ItemSlot.Position.QB, "DAL", 120, 154);
        CreatePlayer("Tua Tagovailoa", ItemSlot.Position.QB, "MIA", 167, 201);
        CreatePlayer("Zach Charbonnet", ItemSlot.Position.RB, "SEA", 114, 100);
        CreatePlayer("Tank Bigsby", ItemSlot.Position.RB, "JAX", 125, 135);
        CreatePlayer("Christian Kirk", ItemSlot.Position.WR, "HOU", 141, 174);
        CreatePlayer("Drake Maye", ItemSlot.Position.QB, "NE", 123, 78);
        CreatePlayer("TreVeyon Henderson", ItemSlot.Position.RB, "NE", 73, 36);
        CreatePlayer("Amari Cooper", ItemSlot.Position.WR, "FA", 187, 215);
        CreatePlayer("Trevor Lawrence", ItemSlot.Position.QB, "JAX", 142, 146);
        CreatePlayer("Matthew Stafford", ItemSlot.Position.QB, "LAR", 164, 231);
        CreatePlayer("Anthony Richardson", ItemSlot.Position.QB, "IND", 180, 192);
        CreatePlayer("Keon Coleman", ItemSlot.Position.WR, "BUF", 119, 105);
        CreatePlayer("Dallas Goedert", ItemSlot.Position.TE, "PHI", 121, 157);
        CreatePlayer("Jerome Ford", ItemSlot.Position.RB, "CLE", 153, 178);
        CreatePlayer("Blake Corum", ItemSlot.Position.RB, "LAR", 177, 156);
        CreatePlayer("Hollywood Brown", ItemSlot.Position.WR, "KC", 146, 171);
        CreatePlayer("Austin Ekeler", ItemSlot.Position.RB, "WAS", 144, 185);
        CreatePlayer("Hunter Henry", ItemSlot.Position.TE, "NE", 150, 213);
        CreatePlayer("Tetairoa McMillan", ItemSlot.Position.WR, "CAR", 65, 33);
        CreatePlayer("Quinshon Judkins", ItemSlot.Position.RB, "CLE", 72, 42);
        CreatePlayer("Rashid Shaheed", ItemSlot.Position.WR, "NO", 134, 145);
        CreatePlayer("Quentin Johnston", ItemSlot.Position.WR, "LAC", 168, 165);
        CreatePlayer("DeAndre Hopkins", ItemSlot.Position.WR, "BAL", 171, 242);
        CreatePlayer("Tyler Allgeier", ItemSlot.Position.RB, "ATL", 152, 167);
        CreatePlayer("Romeo Doubs", ItemSlot.Position.WR, "GB", 183, 183);
        CreatePlayer("Geno Smith", ItemSlot.Position.QB, "LV", 194, 239);
        CreatePlayer("Adam Thielen", ItemSlot.Position.WR, "CAR", 165, 206);
        CreatePlayer("Sam Darnold", ItemSlot.Position.QB, "SEA", 196, 202);
        CreatePlayer("Braelon Allen", ItemSlot.Position.RB, "NYJ", 178, 153);
        CreatePlayer("Jaylen Wright", ItemSlot.Position.RB, "MIA", 179, 158);
        CreatePlayer("Bryce Young", ItemSlot.Position.QB, "CAR", 170, 151);
        CreatePlayer("Marvin Mims", ItemSlot.Position.WR, "DEN", 137, 143);
        CreatePlayer("Pat Freiermuth", ItemSlot.Position.TE, "PIT", 143, 148);
        CreatePlayer("Jaleel McLaughlin", ItemSlot.Position.RB, "DEN", 217, 243);
        CreatePlayer("Justin Fields", ItemSlot.Position.QB, "NYJ", 133, 152);
        CreatePlayer("Rashod Bateman", ItemSlot.Position.WR, "BAL", 160, 159);
        CreatePlayer("Isaac Guerendo", ItemSlot.Position.RB, "SF", 145, 155);
        CreatePlayer("Xavier Legette", ItemSlot.Position.WR, "CAR", 173, 140);
        CreatePlayer("Zach Ertz", ItemSlot.Position.TE, "WAS", 147, 205);
        CreatePlayer("Adonai Mitchell", ItemSlot.Position.WR, "IND", 225, 179);
        CreatePlayer("Dylan Sampson", ItemSlot.Position.RB, "CLE", 172, 141);
        CreatePlayer("Trey Benson", ItemSlot.Position.RB, "ARI", 161, 116);
        CreatePlayer("Cole Kmet", ItemSlot.Position.TE, "CHI", 22, 182);
        CreatePlayer("Tyler Warren", ItemSlot.Position.TE, "IND", 100, 52);
        CreatePlayer("Nick Chubb", ItemSlot.Position.RB, "HOU", 186, 199);
        CreatePlayer("Cade Otton", ItemSlot.Position.TE, "TB", 174, 144);
        CreatePlayer("Marshawn Lloyd", ItemSlot.Position.RB, "GB", 218, 194);
        CreatePlayer("Wan'Dale Robinson", ItemSlot.Position.WR, "NYG", 175, 172);
        CreatePlayer("Demario Douglas", ItemSlot.Position.WR, "NE", 202, 191);
        CreatePlayer("JJ McCarthy", ItemSlot.Position.QB, "MIN", 140, 111);
        CreatePlayer("Michael Wilson", ItemSlot.Position.WR, "ARI", 216, 254);
        CreatePlayer("Luther Burden", ItemSlot.Position.WR, "CHI", 130, 84);
        CreatePlayer("Emeka Egbuka", ItemSlot.Position.WR, "TB", 127, 58);
        CreatePlayer("Roschon Johnson", ItemSlot.Position.RB, "CHI", 182, 196);
        CreatePlayer("Audric Estime", ItemSlot.Position.RB, "DEN", 231, 209);
        CreatePlayer("Matthew Golden", ItemSlot.Position.WR, "GB", 102, 67);
        CreatePlayer("Isaiah Likely", ItemSlot.Position.TE, "BAL", 162, 122);
        NullPlayer("Brandon Aubrey");
        CreatePlayer("Bhayshul Tuten", ItemSlot.Position.RB, "JAX", 136, 121);
        CreatePlayer("Dalton Schultz", ItemSlot.Position.TE, "HOU", 185, 217);
        CreatePlayer("Dontayvion Wicks", ItemSlot.Position.WR, "GB", 230, 218);
        CreatePlayer("Diontae Johnson", ItemSlot.Position.WR, "CLE", 238, 258);
        CreatePlayer("Kaleb Johnson", ItemSlot.Position.RB, "PIT", 89, 53);
        NullPlayer("Cameron Dicker");
        CreatePlayer("Cam Ward", ItemSlot.Position.QB, "TEN", 148, 106);
        CreatePlayer("Antonio Gibson", ItemSlot.Position.RB, "NE", 269, 300);
        CreatePlayer("Cam Skattebo", ItemSlot.Position.RB, "NYG", 105, 80);
        NullPlayer("Justin Tucker");
        CreatePlayer("Joshua Palmer", ItemSlot.Position.WR, "BUF", 181, 232);
        NullPlayer("Ka'imi Fairbairn");
        NullPlayer("Jason Sanders");
        CreatePlayer("Kyle Williams", ItemSlot.Position.WR, "NE", 139, 131);
        NullPlayer("Evan McPherson");
        NullPlayer("Harrison Butker");
        NullPlayer("Tyler Bass");
        CreatePlayer("Ja'Tavion Sanders", ItemSlot.Position.TE, "CAR", 211, 188);
        CreatePlayer("Kimani Vidal", ItemSlot.Position.RB, "LAC", 293, 295);
        CreatePlayer("Mike Gesicki", ItemSlot.Position.TE, "CIN", 176, 238);
        CreatePlayer("Brandin Cooks", ItemSlot.Position.WR, "NO", 261, 347);
        CreatePlayer("Kendre Miller", ItemSlot.Position.RB, "NO", 258, 203);
        CreatePlayer("Tre Harris", ItemSlot.Position.WR, "LAC", 131, 94);
        CreatePlayer("Shedeur Sanders", ItemSlot.Position.QB, "CLE", 209, 189);
        CreatePlayer("Colston Loveland", ItemSlot.Position.TE, "CHI", 112, 65);
        CreatePlayer("Devin Singletary", ItemSlot.Position.RB, "NYG", 271, 294);
        CreatePlayer("Travis Hunter", ItemSlot.Position.WR, "JAX", 69, 30);
        CreatePlayer("Noah Fant", ItemSlot.Position.TE, "SEA", 268, 292);
        CreatePlayer("Michael Penix", ItemSlot.Position.QB, "ATL", 166, 130);
        CreatePlayer("Jaxson Dart", ItemSlot.Position.QB, "NYG", 223, 162);
        CreatePlayer("Aaron Rodgers", ItemSlot.Position.QB, "PIT", 224, 303);
        CreatePlayer("Jalen Milroe", ItemSlot.Position.QB, "SEA", 252, 180);
        CreatePlayer("Tyler Shough", ItemSlot.Position.QB, "NO", 229, 240);
        CreatePlayer("Jordan Mason", ItemSlot.Position.RB, "MIN", 128, 147);
        CreatePlayer("Jaydon Blue", ItemSlot.Position.RB, "DAL", 135, 124);
        CreatePlayer("Ray Davis", ItemSlot.Position.RB, "BUF", 149, 161);
        CreatePlayer("Devin Neal", ItemSlot.Position.RB, "NO", 184, 150);
        CreatePlayer("Kareem Hunt", ItemSlot.Position.RB, "KC", 197, 248);
        CreatePlayer("Will Shipley", ItemSlot.Position.RB, "PHI", 199, 244);
        CreatePlayer("RJ Harvey", ItemSlot.Position.RB, "DEN", 66, 50);
        CreatePlayer("Ollie Gordon", ItemSlot.Position.RB, "MIA", 233, 177);
        CreatePlayer("DJ Giddens", ItemSlot.Position.RB, "IND", 214, 181);
        CreatePlayer("Trevor Etienne", ItemSlot.Position.RB, "CAR", 234, 184);
        CreatePlayer("Jordan James", ItemSlot.Position.RB, "SF", 212, 187);
        CreatePlayer("Jonathon Brooks", ItemSlot.Position.RB, "CAR", 517, 193);
        CreatePlayer("Jarquez Hunter", ItemSlot.Position.RB, "LAR", 201, 195);
        CreatePlayer("Woody Marks", ItemSlot.Position.RB, "HOU", 220, 200);
        CreatePlayer("Jayden Higgins", ItemSlot.Position.WR, "HOU", 128, 93);
        CreatePlayer("Cedric Tillman", ItemSlot.Position.WR, "CLE", 192, 170);
        CreatePlayer("Pat Bryant", ItemSlot.Position.WR, "DEN", 198, 175);
        CreatePlayer("Jalen Royals", ItemSlot.Position.WR, "KC", 204, 163);
        CreatePlayer("Jaylin Noel", ItemSlot.Position.WR, "HOU", 205, 136);
        CreatePlayer("Jalen Coker", ItemSlot.Position.WR, "CAR", 210, 169);
        CreatePlayer("Tyler Lockett", ItemSlot.Position.WR, "TEN", 213, 272);
        CreatePlayer("Elic Ayomanor", ItemSlot.Position.WR, "TEN", 219, 173);
        CreatePlayer("Darius Slayton", ItemSlot.Position.WR, "NYG", 226, 273);
        CreatePlayer("Roman Wilson", ItemSlot.Position.WR, "PIT", 228, 204);
        CreatePlayer("Jack Bech", ItemSlot.Position.WR, "LV", 157, 115);
        CreatePlayer("Jauan Jennings", ItemSlot.Position.WR, "SF", 126, 127);
        CreatePlayer("Tank Dell", ItemSlot.Position.WR, "HOU", 164, 278);
        CreatePlayer("Tory Horton", ItemSlot.Position.WR, "SEA", 254, 197);
        CreatePlayer("Isaac TeSlaa", ItemSlot.Position.WR, "DET", 255, 198);
        CreatePlayer("Xavier Restrepo", ItemSlot.Position.WR, "TEN", 247, 207);
        CreatePlayer("Troy Franklin", ItemSlot.Position.WR, "DEN", 267, 210);
        CreatePlayer("Alec Pierce", ItemSlot.Position.WR, "IND", 200, 211);
        CreatePlayer("Mason Taylor", ItemSlot.Position.TE, "NYJ", 169, 132);
        CreatePlayer("Brenton Strange", ItemSlot.Position.TE, "JAX", 193, 186);
        CreatePlayer("Terrance Ferguson", ItemSlot.Position.TE, "LAR", 207, 160);
        CreatePlayer("Elijah Arroyo", ItemSlot.Position.TE, "SEA", 227, 166);
        CreatePlayer("Harold Fannin", ItemSlot.Position.TE, "CLE", 236, 176);
        CreatePlayer("Theo Johnson", ItemSlot.Position.TE, "NYG", 221, 212);
        CreatePlayer("Ben Sinnott", ItemSlot.Position.TE, "WAS", 266, 190);

        InitialPlayers = InitialPlayers.OrderBy(i => i.adpRanking).ToList();
    }

    void NullPlayer(string name)
    {
        _playerIdNumberCount++;
    }

    void CreatePlayer(string name, ItemSlot.Position position, string teamAbrev, int regAdp, int dynastyAdp)
    {
        _playerIdNumberCount++;
        int ranking = IsDynasty ? dynastyAdp : regAdp;

        if (ranking > 230 || position == ItemSlot.Position.OTHER)
        {
            return;
        }

        string[] split = name.Split(' ');
        if (name == "Amon-Ra St. Brown")
        {
            split[0] = "Amon-Ra";
            split[1] = "St. Brown";
        }

        MetaData metaData = new MetaData() { first_name = split[0], last_name = split[1], team = teamAbrev, position = position.ToString() };
        DraftPick pick = new()
        {
            metadata = metaData,
            adpRanking = ranking,
            idNumber = _playerIdNumberCount
        };

        InitialPlayers.Add(pick);
    }
}
