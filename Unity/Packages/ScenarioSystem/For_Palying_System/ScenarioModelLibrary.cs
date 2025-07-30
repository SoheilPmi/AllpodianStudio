using System.Collections.Generic;

public static class ScenarioModelLibrary
{
    // List of 70 ready‐to‐use scenario models
    public static readonly List<ScenarioModelDefinition> Models = new List<ScenarioModelDefinition>()
    {
        // 1
        new ScenarioModelDefinition {
            Id = "SpawnWave",
            Description = "Spawn a wave of enemies every N seconds until score >= M",
            Template =
@"loop every={interval} seconds until score >= {scoreThreshold}:
  action Spawn tag={enemyTag} count={count};
end",
            ParameterKeys = new List<string>{ "interval", "scoreThreshold", "enemyTag", "count" }
        },
        // 2
        new ScenarioModelDefinition {
            Id = "TimedSpawn",
            Description = "Spawn X enemies after Y seconds delay",
            Template =
@"action Wait seconds={delay};
action Spawn tag={enemyTag} count={count};",
            ParameterKeys = new List<string>{ "delay", "enemyTag", "count" }
        },
        // 3
        new ScenarioModelDefinition {
            Id = "CollectItems",
            Description = "Complete mission when player collects N items of type T",
            Template =
@"when {itemTag}Collected >= {targetCount} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "itemTag", "targetCount", "missionId" }
        },
        // 4
        new ScenarioModelDefinition {
            Id = "EscortMission",
            Description = "Escort NPC from A to B; fail on NPC death",
            Template =
@"action Spawn prefab={npcPrefab} location={startPoint};
eventListener onDeath target={npcPrefab} ->
  action FailMission id={missionId};",
            ParameterKeys = new List<string>{ "npcPrefab", "startPoint", "missionId" }
        },
        // 5
        new ScenarioModelDefinition {
            Id = "StealthDetect",
            Description = "Trigger alarm and spawn guards on detection",
            Template =
@"eventListener onEnter zone={zoneTag} ->
  action PlaySound id={alarmSound};
  action Spawn tag={guardTag} count={guardCount};",
            ParameterKeys = new List<string>{ "zoneTag", "alarmSound", "guardTag", "guardCount" }
        },
        // 6
        new ScenarioModelDefinition {
            Id = "AreaDefense",
            Description = "Defend area for T seconds against waves of enemies",
            Template =
@"action Spawn tag={enemyTag} count={initialCount};
loop every={waveInterval} seconds times={waveCount}:
  action Spawn tag={enemyTag} count={perWaveCount};
end
when timer >= {defendTime} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "enemyTag", "initialCount", "waveInterval", "waveCount", "perWaveCount", "defendTime", "missionId" }
        },
        // 7
        new ScenarioModelDefinition {
            Id = "BossPhase",
            Description = "Switch boss AI when health <= X%",
            Template =
@"condition {bossId}.health <= {threshold} ->
  action SetAIBehavior target={bossId} pattern={newPattern};",
            ParameterKeys = new List<string>{ "bossId", "threshold", "newPattern" }
        },
        // 8
        new ScenarioModelDefinition {
            Id = "BranchDialogue",
            Description = "Branch story based on player choice",
            Template =
@"action ShowChoices options=[\"{opt1}\",\"{opt2}\"];
condition choice == \"{opt1}\" ->
  action UnlockPath id={path1};
else ->
  action UnlockPath id={path2};",
            ParameterKeys = new List<string>{ "opt1", "opt2", "path1", "path2" }
        },
        // 9
        new ScenarioModelDefinition {
            Id = "CinematicCutscene",
            Description = "Play cutscene then restore control",
            Template =
@"action DisablePlayerControl;
action PlayCutscene id={cutsceneId};
action Wait seconds={duration};
action EnablePlayerControl;",
            ParameterKeys = new List<string>{ "cutsceneId", "duration" }
        },
        // 10
        new ScenarioModelDefinition {
            Id = "ChangeWeather",
            Description = "Transition to weather W over T seconds",
            Template =
@"action ChangeWeather type={weatherType} duration={transitionTime};",
            ParameterKeys = new List<string>{ "weatherType", "transitionTime" }
        },
        // 11
        new ScenarioModelDefinition {
            Id = "TimedChallenge",
            Description = "Complete X objectives within Y seconds",
            Template =
@"action StartTimer seconds={timeLimit};
when objectivesCompleted >= {targetCount} && timer < {timeLimit} ->
  action CompleteMission id={missionId};
else when timer >= {timeLimit} ->
  action FailMission id={missionId};",
            ParameterKeys = new List<string>{ "timeLimit", "targetCount", "missionId" }
        },
        // 12
        new ScenarioModelDefinition {
            Id = "RaceObjective",
            Description = "Reach checkpoint before timer ends",
            Template =
@"action StartTimer seconds={timeLimit};
eventListener onEnter zone={checkpointTag} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "timeLimit", "checkpointTag", "missionId" }
        },
        // 13
        new ScenarioModelDefinition {
            Id = "PuzzleUnlock",
            Description = "Solve puzzle with N switches",
            Template =
@"loop every=0.5 seconds times=∞:
  condition switchesActivated >= {switchCount} ->
    action UnlockDoor id={doorId};
    break;
end",
            ParameterKeys = new List<string>{ "switchCount", "doorId" }
        },
        // 14
        new ScenarioModelDefinition {
            Id = "AreaScan",
            Description = "Scan N items in region",
            Template =
@"loop every=1 seconds until scannedCount >= {scanCount}:
  action ScanArea zone={zoneTag};
end
action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "scanCount", "zoneTag", "missionId" }
        },
        // 15
        new ScenarioModelDefinition {
            Id = "ResourceGather",
            Description = "Gather N resources of type T",
            Template =
@"when {resourceTag}Gathered >= {targetCount} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "resourceTag", "targetCount", "missionId" }
        },
        // 16
        new ScenarioModelDefinition {
            Id = "TerritoryCapture",
            Description = "Hold area for T seconds to capture",
            Template =
@"eventListener onEnter zone={zoneTag} ->
  action StartTimer seconds={holdTime};
eventListener onExit zone={zoneTag} ->
  action ResetTimer;
when timer >= {holdTime} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "zoneTag", "holdTime", "missionId" }
        },
        // 17
        new ScenarioModelDefinition {
            Id = "BaseBuilding",
            Description = "Construct X structures",
            Template =
@"when structuresBuilt >= {structureCount} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "structureCount", "missionId" }
        },
        // 18
        new ScenarioModelDefinition {
            Id = "SupplyDrop",
            Description = "Call a supply drop at position after delay",
            Template =
@"action Wait seconds={delay};
action Spawn prefab={supplyPrefab} location={dropPoint};",
            ParameterKeys = new List<string>{ "delay", "supplyPrefab", "dropPoint" }
        },
        // 19
        new ScenarioModelDefinition {
            Id = "SurvivalEndurance",
            Description = "Survive enemy waves for T seconds",
            Template =
@"action StartTimer seconds={surviveTime};
loop every={spawnInterval} seconds until timer >= {surviveTime}:
  action Spawn tag={enemyTag} count={perWaveCount};
end
when timer >= {surviveTime} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "surviveTime", "spawnInterval", "enemyTag", "perWaveCount", "missionId" }
        },
        // 20
        new ScenarioModelDefinition {
            Id = "InfiltrationMission",
            Description = "Enter area, disable alarm, and exit",
            Template =
@"eventListener onEnter zone={entryZone} ->
  action DisableAlarm id={alarmId};
eventListener onEnter zone={exitZone} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "entryZone", "alarmId", "exitZone", "missionId" }
        },
        // 21
        new ScenarioModelDefinition {
            Id = "FetchQuest",
            Description = "Retrieve item T from location L",
            Template =
@"action ShowMessage text=\"Retrieve {itemTag} from {locationTag}\";
when playerInteracts tag={itemTag} && playerLocation == \"{locationTag}\" ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "itemTag", "locationTag", "missionId" }
        },
        // 22
        new ScenarioModelDefinition {
            Id = "HackTerminal",
            Description = "Hack terminal within time limit",
            Template =
@"action StartTimer seconds={timeLimit};
eventListener onHackSuccess target={terminalId} ->
  action CompleteMission id={missionId}; 
when timer >= {timeLimit} ->
  action FailMission id={missionId};",
            ParameterKeys = new List<string>{ "timeLimit", "terminalId", "missionId" }
        },
        // 23
        new ScenarioModelDefinition {
            Id = "ProtectAsset",
            Description = "Protect asset until timer expires",
            Template =
@"action StartTimer seconds={protectTime};
eventListener onDamage target={assetId} ->
  action ShowMessage text=\"Asset under attack!\";
when timer >= {protectTime} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "protectTime", "assetId", "missionId" }
        },
        // 24
        new ScenarioModelDefinition {
            Id = "DataRetrieval",
            Description = "Retrieve data from N terminals",
            Template =
@"when terminalsHacked >= {targetCount} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "targetCount", "missionId" }
        },
        // 25
        new ScenarioModelDefinition {
            Id = "EscortMultiple",
            Description = "Escort multiple NPCs; fail if any die",
            Template =
@"action Spawn prefab={npcGroupPrefab} location={startPoint};
eventListener onDeath targetGroup={npcGroupPrefab} ->
  action FailMission id={missionId};",
            ParameterKeys = new List<string>{ "npcGroupPrefab", "startPoint", "missionId" }
        },
        // 26
        new ScenarioModelDefinition {
            Id = "StealthKill",
            Description = "Eliminate target silently",
            Template =
@"eventListener onDetect target={targetId} ->
  action FailMission id={missionId};
eventListener onKillSilent target={targetId} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "targetId", "missionId" }
        },
        // 27
        new ScenarioModelDefinition {
            Id = "AmbushTrigger",
            Description = "Trigger an ambush when entering zone",
            Template =
@"eventListener onEnter zone={zoneTag} ->
  action Spawn tag={enemyTag} count={count};
  action ShowMessage text=\"Ambush!\";",
            ParameterKeys = new List<string>{ "zoneTag", "enemyTag", "count" }
        },
        // 28
        new ScenarioModelDefinition {
            Id = "TrapDisarm",
            Description = "Disarm N traps to proceed",
            Template =
@"when trapsDisarmed >= {targetCount} ->
  action UnlockDoor id={doorId};",
            ParameterKeys = new List<string>{ "targetCount", "doorId" }
        },
        // 29
        new ScenarioModelDefinition {
            Id = "SabotageMachine",
            Description = "Destroy machine at location",
            Template =
@"action Spawn prefab={machinePrefab} location={machinePoint};
eventListener onDestroy target={machinePrefab} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "machinePrefab", "machinePoint", "missionId" }
        },
        // 30
        new ScenarioModelDefinition {
            Id = "TimedDemolition",
            Description = "Plant bomb and detonate after delay",
            Template =
@"action PlantBomb at={bombPoint};
action Wait seconds={delay};
action DetonateBomb id={bombId};",
            ParameterKeys = new List<string>{ "bombPoint", "delay", "bombId" }
        },
        // 31
        new ScenarioModelDefinition {
            Id = "KeyAndLock",
            Description = "Find key to open locked door",
            Template =
@"when playerInteracts tag={keyTag} ->
  action ShowMessage text=\"Key acquired\";
eventListener onInteract target={doorId} when hasKey ->
  action UnlockDoor id={doorId};",
            ParameterKeys = new List<string>{ "keyTag", "doorId" }
        },
        // 32
        new ScenarioModelDefinition {
            Id = "DataDecrypt",
            Description = "Decrypt code before timer runs out",
            Template =
@"action StartTimer seconds={timeLimit};
eventListener onDecryptSuccess target={deviceId} ->
  action CompleteMission id={missionId};
when timer >= {timeLimit} ->
  action FailMission id={missionId};",
            ParameterKeys = new List<string>{ "timeLimit", "deviceId", "missionId" }
        },
        // 33
        new ScenarioModelDefinition {
            Id = "DecoyDrop",
            Description = "Drop decoy to distract guards",
            Template =
@"action Spawn prefab={decoyPrefab} location={dropPoint};
action StartTimer seconds={decoyDuration};",
            ParameterKeys = new List<string>{ "decoyPrefab", "dropPoint", "decoyDuration" }
        },
        // 34
        new ScenarioModelDefinition {
            Id = "DynamicObstacle",
            Description = "Navigate obstacles that move every T seconds",
            Template =
@"loop every={moveInterval} seconds times={waveCount}:
  action MoveObstacles tag={obstacleTag};
end",
            ParameterKeys = new List<string>{ "moveInterval", "waveCount", "obstacleTag" }
        },
        // 35
        new ScenarioModelDefinition {
            Id = "ChaseTarget",
            Description = "Chase a moving target until caught",
            Template =
@"action Spawn prefab={targetPrefab} at={startPoint};
eventListener onCatch target={targetPrefab} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "targetPrefab", "startPoint", "missionId" }
        },
        // 36
        new ScenarioModelDefinition {
            Id = "ProtectNPC",
            Description = "Protect an NPC from waves of attackers",
            Template =
@"action Spawn prefab={npcPrefab} location={spawnPoint};
loop every={spawnInterval} seconds until npcHealth <= 0:
  action Spawn tag={enemyTag} count={perWaveCount};
end",
            ParameterKeys = new List<string>{ "npcPrefab", "spawnPoint", "spawnInterval", "enemyTag", "perWaveCount" }
        },
        // 37
        new ScenarioModelDefinition {
            Id = "MultiPhaseBoss",
            Description = "Multi-phase boss fight with health thresholds",
            Template =
@"condition boss.health <= {phaseOneThreshold} -> action SetAIBehavior target=boss pattern=PhaseTwo;
condition boss.health <= {phaseTwoThreshold} -> action SetAIBehavior target=boss pattern=FinalPhase;",
            ParameterKeys = new List<string>{ "phaseOneThreshold", "phaseTwoThreshold" }
        },
        // 38
        new ScenarioModelDefinition {
            Id = "ScoutArea",
            Description = "Discover N points of interest",
            Template =
@"when pointsDiscovered >= {targetCount} ->
  action CompleteMission id={missionId};",
            ParameterKeys = new List<string>{ "targetCount", "missionId" }
        },
        // 39
        new ScenarioModelDefinition {
            Id = "DayNightCycle",
            Description = "Switch between day and night after intervals",
            Template =
@"loop every={dayDuration} seconds:
  action ChangeTimeOfDay to=Night;
  action Wait seconds={nightDuration};
  action ChangeTimeOfDay to=Day;
end",
            ParameterKeys = new List<string>{ "dayDuration", "nightDuration" }
        },
        // 40
        new ScenarioModelDefinition {
            Id = "WeatherHazard",
            Description = "Activate hazardous weather for T seconds",
            Template =
@"action ChangeWeather type={hazardType} duration={startTransition};
action StartTimer seconds={hazardDuration};
action ChangeWeather type=Clear duration={endTransition};",
            ParameterKeys = new List<string>{ "hazardType", "startTransition", "hazardDuration", "endTransition" }
        },
        // 41
        new ScenarioModelDefinition {
            Id = "DynamicDifficulty",
            Description = "Adjust spawn rate based on player score",
            Template =
@"loop every=10 seconds times=∞:
  action SetVariable name=spawnRate value={baseRate}+score/{scale};
  action UpdateSpawnInterval seconds=spawnRate;
end",
            ParameterKeys = new List<string>{ "baseRate", "scale" }
        }
    };
}
      
