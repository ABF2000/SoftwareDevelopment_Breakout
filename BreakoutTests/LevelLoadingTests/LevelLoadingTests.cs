using NUnit.Framework;
using DIKUArcade.Events;
using Breakout;
using Breakout.Blocks;
using Breakout.LevelLoading;
using System.Collections.Generic;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using System.IO;
namespace BreakoutTests;

// TODO: More rigid testing
public class LevelLoadingTests
{
    // private EntityContainer<Block> blockFormation;

    private LevelLoader loader;

    private List<FormationData> map;

    private string emptyLevelPathName;

    private string invalidLevelPathName;

    private string validLevelPathName;

    private string dir;

    [SetUp]
    public void Setup()
    {
        dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        loader = new LevelLoader();
    }

    [Test]
    public void EmptyLevelTest()
    {
        emptyLevelPathName = dir[..(dir.Length - 16)] + Path.Combine("Assets", "Levels", "empty-level.txt");
        loader.ReadFile(emptyLevelPathName);
        map = loader.GetMap();
        Assert.AreEqual(0, map.Count);
    }

    [Test]
    public void InvalidLevelTest(){
        invalidLevelPathName = dir[..(dir.Length - 16)] + Path.Combine("Assets", "Levels", "invalid-level.txt");
        loader.ReadFile(invalidLevelPathName);
        map = loader.GetMap();
        Assert.AreEqual(0, map.Count);
    }

    [Test]
    public void ValidLevelTest(){
        validLevelPathName = dir[..(dir.Length - 16)] + Path.Combine("Assets", "Levels", "level1.txt");
        loader.ReadFile(validLevelPathName);
        map = loader.GetMap();
        Assert.AreEqual(76, map.Count);
    }

    [Test]
    public void DataStructureTest(){
        validLevelPathName = dir[..(dir.Length - 16)] + Path.Combine("Assets", "Levels", "level1.txt");
        loader.ReadFile(validLevelPathName);
        map = loader.GetMap();
        Assert.True(map is List<FormationData>);
        Assert.True(map.Count > 0);
    }
    
    [Test]
    public void InvalidPathTest(){
        string invalidPath = "hej";
        loader.ReadFile(invalidPath);
        map = loader.GetMap();
        Assert.AreEqual(0, map.Count);

    }
    
}