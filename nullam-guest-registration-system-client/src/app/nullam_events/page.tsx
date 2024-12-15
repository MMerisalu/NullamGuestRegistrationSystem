"use client";
import React, { useEffect, useState } from "react";
import Link from "next/link";
import IEvent from "../domain/IEvent";
import { EventService } from "@/services/EventService";
import {format} from 'date-fns'

