using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ServerStatusStruct
{
    public CPU cpu;
    public FanStatus fanStatus = new FanStatus();
    public MotherBoardStatus motherBoardStatus = new MotherBoardStatus();
    public OperatingSystem os = new OperatingSystem();
    public List<HardDiscStatus> disks = new List<HardDiscStatus>();

    public void setCPU(ProcessorStatus status) { cpu = new CPU(status); }

    public bool isOperational(){ return cpu != null && fanStatus != null && motherBoardStatus != null && disks != null; }
}