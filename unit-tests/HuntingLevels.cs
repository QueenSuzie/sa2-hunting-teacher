using sa2_hunting_teacher;
using sa2_hunting_teacher.Knuckles;
using sa2_hunting_teacher.Rouge;
using System.Runtime.CompilerServices;

namespace unit_tests;

public class HuntingLevels {
	private SA2Manager sa2;

	public HuntingLevels() {
		this.sa2 = (SA2Manager)RuntimeHelpers.GetUninitializedObject(typeof(SA2Manager));
	}

	[Fact]
	public void WildCanyonIncludesAllPieces() {
		HuntingLevels.AssertLevelSequenceContainsAllPieces(new WildCanyon(this.sa2, 1), WildCanyon.PieceToHint.ToList());
	}

	[Fact]
	public void PumpkinHillIncludesAllPieces() {
		HuntingLevels.AssertLevelSequenceContainsAllPieces(new PumpkinHill(this.sa2, 1), PumpkinHill.PieceToHint.ToList());
	}

	[Fact]
	public void AquaticMineIncludesAllPieces() {
		HuntingLevels.AssertLevelSequenceContainsAllPieces(new AquaticMine(this.sa2, 1), AquaticMine.PieceToHint.ToList());
	}

	[Fact]
	public void DeathChamberIncludesAllPieces() {
		HuntingLevels.AssertLevelSequenceContainsAllPieces(new DeathChamber(this.sa2, 1), DeathChamber.PieceToHint.ToList());
	}

	[Fact]
	public void MeteorHerdIncludesAllPieces() {
		HuntingLevels.AssertLevelSequenceContainsAllPieces(new MeteorHerd(this.sa2, 1), MeteorHerd.PieceToHint.ToList());
	}

	[Fact]
	public void DryLagoonIncludesAllPieces() {
		HuntingLevels.AssertLevelSequenceContainsAllPieces(new DryLagoon(this.sa2, 1), DryLagoon.PieceToHint.ToList());
	}

	[Fact]
	public void EggQuartersIncludesAllPieces() {
		HuntingLevels.AssertLevelSequenceContainsAllPieces(new EggQuarters(this.sa2, 1), EggQuarters.PieceToHint.ToList());
	}

	[Fact]
	public void SecurityHallIncludesAllPieces() {
		HuntingLevels.AssertLevelSequenceContainsAllPieces(new SecurityHall(this.sa2, 1), SecurityHall.PieceToHint.ToList());
	}

	[Fact]
	public void MadSpaceIncludesAllPieces() {
		HuntingLevels.AssertLevelSequenceContainsAllPieces(new MadSpace(this.sa2, 1), MadSpace.PieceToHint.ToList());
	}

	private static void AssertLevelSequenceContainsAllPieces(HuntingLevel level, List<KeyValuePair<int, string>> pieces) {
		Set[] sequence = (Set[])level.GetType().GetProperty(
			"Sequence",
			System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
		)!.GetValue(level)!;

		pieces.ForEach(piece => {
			var match = sequence.FirstOrDefault(set => set.P1Id == piece.Key || set.P2Id == piece.Key || set.P3Id == piece.Key);
			Assert.True(match != null, $"Expected Piece with Id=0x{piece.Key:x4} was not found in the sequence for {level}");
		});
	}
}