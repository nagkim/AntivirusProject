# Virus Detection System

## Project Description

This project aims to develop a virus detection system for Windows operating systems to enhance computer security by detecting and mitigating malware threats. The system utilizes various techniques such as monitoring scheduled tasks, anomaly detection through network monitoring, manual file scanning, and startup file analysis.

## Project Features

### Monitoring Scheduled Tasks

The system monitors scheduled tasks configured on the Windows operating system using the IBM X-Force Exchange API. This helps in identifying any malicious activities scheduled to occur.

### Anomaly Detection

Anomaly detection is implemented through continuous network monitoring. The system identifies unusual patterns or behaviors within network traffic that may indicate the presence of malware.

### Manual File Scanning

Users can initiate manual file scanning to detect malware in specific files of interest. This feature allows for targeted scanning and ensures thorough malware detection.

### Startup File Analysis

The system retrieves registry keys and analyzes startup files using the Hybrid Analysis API to ensure the safety of system startup processes. This helps in preventing malware from executing during system startup.
